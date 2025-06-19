using System.ComponentModel.DataAnnotations;

namespace StudentEventManagement.Application.DTOs
{
    public class CreateRegistrationDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid EventId { get; set; }
    }
}