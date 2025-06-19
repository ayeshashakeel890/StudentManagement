using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Application.DTOs;
using StudentEventManagement.Application.Interfaces;
using StudentEventManagement.Domain.Entities;
using StudentEventManagement.Infrastructure.Data;

namespace StudentEventManagement.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ApplicationDbContext _context;

        public RegistrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RegisterStudentForEventAsync(CreateRegistrationDto createRegistrationDto)
        {
            // 1. Check if the student and event exist
            var studentExists = await _context.Students.AnyAsync(s => s.Id == createRegistrationDto.StudentId);
            if (!studentExists)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            var eventExists = await _context.Events.AnyAsync(e => e.Id == createRegistrationDto.EventId);
            if (!eventExists)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            // 2. Check for duplicate registration (this relies on the unique index we created)
            var isAlreadyRegistered = await _context.Registrations
                .AnyAsync(r => r.StudentId == createRegistrationDto.StudentId && r.EventId == createRegistrationDto.EventId);

            if (isAlreadyRegistered)
            {
                // We throw a custom exception type that our controller can catch.
                // Let's create this exception class first. For now, we'll throw a generic one.
                throw new InvalidOperationException("Student is already registered for this event.");
            }

            // 3. Create and save the new registration
            var registration = new Registration
            {
                StudentId = createRegistrationDto.StudentId,
                EventId = createRegistrationDto.EventId
                // The Id and RegistrationDate will be set by default
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
        }
    }
}