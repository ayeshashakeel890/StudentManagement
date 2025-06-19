using AutoMapper;
using StudentEventManagement.Application.DTOs;
using StudentEventManagement.Domain.Entities;

namespace StudentEventManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Entity to DTO
            CreateMap<Event, EventDto>();

            // From DTO to Entity
            CreateMap<CreateEventDto, Event>();
        }
    }
}