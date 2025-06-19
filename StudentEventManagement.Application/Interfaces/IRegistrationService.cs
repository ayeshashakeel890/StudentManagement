using StudentEventManagement.Application.DTOs;

namespace StudentEventManagement.Application.Interfaces
{
    public interface IRegistrationService
    {
        Task RegisterStudentForEventAsync(CreateRegistrationDto createRegistrationDto);
    }
}