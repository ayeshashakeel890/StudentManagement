using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Application.DTOs;
using StudentEventManagement.Application.Interfaces;
using StudentEventManagement.Domain.Entities;
using StudentEventManagement.Infrastructure.Data;

namespace StudentEventManagement.Application.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EventDto> CreateEventAsync(CreateEventDto createEventDto)
        {
            var newEvent = _mapper.Map<Event>(createEventDto);
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventDto>(newEvent);
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync(string? searchQuery, string? sortBy)
        {
            // Start with the base query
            var query = _context.Events.AsQueryable();

            // 1. Filtering (Search)
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var lowerCaseQuery = searchQuery.ToLower();
                query = query.Where(e =>
                    e.Name.ToLower().Contains(lowerCaseQuery) ||
                    e.Venue.ToLower().Contains(lowerCaseQuery));
            }

            // 2. Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "date":
                        query = query.OrderBy(e => e.EventDate);
                        break;
                    case "date_desc":
                        query = query.OrderByDescending(e => e.EventDate);
                        break;
                    case "name":
                        query = query.OrderBy(e => e.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(e => e.Name);
                        break;
                    default:
                        // Default sort by upcoming date if sort key is invalid
                        query = query.OrderBy(e => e.EventDate);
                        break;
                }
            }
            else
            {
                // Default sort by upcoming date if no sort key is provided
                query = query.OrderBy(e => e.EventDate);
            }

            // 3. Execute the query
            var events = await query.ToListAsync();

            // 4. Map to DTOs and return
            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task<EventDto> GetEventByIdAsync(Guid id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            return _mapper.Map<EventDto>(eventEntity);
        }

        public async Task UpdateEventAsync(Guid id, CreateEventDto updateEventDto)
        {
            var eventToUpdate = await _context.Events.FindAsync(id);
            if (eventToUpdate != null)
            {
                // Use AutoMapper to update the existing entity from the DTO
                _mapper.Map(updateEventDto, eventToUpdate);
                await _context.SaveChangesAsync();
            }
        }
    }
}