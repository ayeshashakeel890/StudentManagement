public class EventDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Add this
    public string Description { get; set; } = string.Empty; // Add this
    public string Venue { get; set; } = string.Empty; // Add this
    public DateTime EventDate { get; set; }
}