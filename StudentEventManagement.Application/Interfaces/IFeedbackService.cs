using StudentEventManagement.Application.DTOs;

namespace StudentEventManagement.Application.Interfaces
{
    public interface IFeedbackService
    {
        Task SubmitFeedbackAsync(CreateFeedbackDto createFeedbackDto);
    }
}