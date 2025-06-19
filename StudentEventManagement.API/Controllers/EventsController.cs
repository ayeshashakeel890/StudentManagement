using Microsoft.AspNetCore.Mvc;
using StudentEventManagement.Application.DTOs;
using StudentEventManagement.Application.Interfaces;

namespace StudentEventManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        
        // GET: api/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents([FromQuery] string? search, [FromQuery] string? sort)
        {
            var events = await _eventService.GetEventsAsync(search, sort);
            return Ok(events);
        }

        // GET: api/events/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(Guid id)
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);
            if (eventDto == null)
            {
                return NotFound(); // Returns 404 Not Found
            }
            return Ok(eventDto);
        }

        // POST: api/events
        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto createEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns 400 Bad Request if validation fails
            }

            var newEvent = await _eventService.CreateEventAsync(createEventDto);
            // Returns a 201 Created status with a link to the new resource
            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
        }

        // PUT: api/events/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, CreateEventDto updateEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _eventService.UpdateEventAsync(id, updateEventDto);
            return NoContent(); // Returns 204 No Content, indicating success
        }

        // DELETE: api/events/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent(); // Returns 204 No Content
        }
    }
}