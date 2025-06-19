using System.ComponentModel.DataAnnotations;

namespace StudentEventManagement.Domain.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid StudentId { get; set; } // To know who gave the feedback
        public int Rating { get; set; } // e.g., 1 to 5
        public string Comment { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public Event Event { get; set; }
    }
}