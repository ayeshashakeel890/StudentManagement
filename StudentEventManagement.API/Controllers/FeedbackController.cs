using Microsoft.AspNetCore.Mvc;
using StudentEventManagement.Application.DTOs;
using StudentEventManagement.Application.Interfaces;

namespace StudentEventManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<IActionResult> SubmitFeedback(CreateFeedbackDto createFeedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _feedbackService.SubmitFeedbackAsync(createFeedbackDto);
                return Ok(new { message = "Feedback submitted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 for business rule violations
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}