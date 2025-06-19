namespace StudentEventManagement.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}