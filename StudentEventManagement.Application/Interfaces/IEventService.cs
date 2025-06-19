using StudentEventManagement.Application.DTOs;

namespace StudentEventManagement.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEventsAsync(string? searchQuery, string? sortBy);
        Task<EventDto> GetEventByIdAsync(Guid id);
        Task<EventDto> CreateEventAsync(CreateEventDto createEventDto);
        Task UpdateEventAsync(Guid id, CreateEventDto updateEventDto);
        Task DeleteEventAsync(Guid id);
    }
}