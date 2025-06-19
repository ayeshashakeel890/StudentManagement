using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Application.DTOs;
using StudentEventManagement.Application.Interfaces;
using StudentEventManagement.Domain.Entities;
using StudentEventManagement.Infrastructure.Data;

namespace StudentEventManagement.Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SubmitFeedbackAsync(CreateFeedbackDto createFeedbackDto)
        {
            // 1. Find the event
            var relevantEvent = await _context.Events.FindAsync(createFeedbackDto.EventId);
            if (relevantEvent == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            // 2. *** BUSINESS RULE ***: Check if the event has passed
            if (relevantEvent.EventDate >= DateTime.UtcNow)
            {
                throw new InvalidOperationException("Feedback can only be submitted for past events.");
            }

            // 3. Optional: Check if the student was actually registered for the event
            var wasRegistered = await _context.Registrations
                .AnyAsync(r => r.StudentId == createFeedbackDto.StudentId && r.EventId == createFeedbackDto.EventId);

            if (!wasRegistered)
            {
                throw new InvalidOperationException("Feedback can only be submitted by registered attendees.");
            }

            // 4. Optional: Check if feedback has already been submitted by this student for this event
            var hasAlreadySubmitted = await _context.Feedbacks
                .AnyAsync(f => f.StudentId == createFeedbackDto.StudentId && f.EventId == createFeedbackDto.EventId);

            if (hasAlreadySubmitted)
            {
                throw new InvalidOperationException("You have already submitted feedback for this event.");
            }

            // 5. Create and save the new feedback
            var feedback = new Feedback
            {
                EventId = createFeedbackDto.EventId,
                StudentId = createFeedbackDto.StudentId,
                Rating = createFeedbackDto.Rating,
                Comment = createFeedbackDto.Comment
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }
    }
}