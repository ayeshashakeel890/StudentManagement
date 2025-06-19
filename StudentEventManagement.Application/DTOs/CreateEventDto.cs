using System.ComponentModel.DataAnnotations;

namespace StudentEventManagement.Application.DTOs
{
    public class CreateEventDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Venue { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
    }
}