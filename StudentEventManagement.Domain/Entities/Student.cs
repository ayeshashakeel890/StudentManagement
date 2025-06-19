using StudentEventManagement.Domain.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Add this
    public string Email { get; set; } = string.Empty; // Add this

    // Navigation Property
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}