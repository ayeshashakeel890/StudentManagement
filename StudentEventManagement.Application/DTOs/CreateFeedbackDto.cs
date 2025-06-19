using System.ComponentModel.DataAnnotations;

namespace StudentEventManagement.Application.DTOs
{
    public class CreateFeedbackDto
    {
        [Required]
        public Guid EventId { get; set; }

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        [Range(1, 5)] // Assuming a rating scale of 1 to 5
        public int Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; }
    }
}