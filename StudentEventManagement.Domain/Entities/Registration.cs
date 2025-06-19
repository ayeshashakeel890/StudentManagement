namespace StudentEventManagement.Domain.Entities
{
    public class Registration
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid EventId { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Student Student { get; set; }
        public Event Event { get; set; }
    }
}