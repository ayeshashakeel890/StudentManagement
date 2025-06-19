using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Domain.Entities;

namespace StudentEventManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure a unique index on Registration to prevent duplicate registrations
            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.StudentId, r.EventId })
                .IsUnique();

            // Configure the relationship between Registration and Student
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Student)
                .WithMany(s => s.Registrations)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a student if they have registrations

            // Configure the relationship between Registration and Event
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting an event if it has registrations

            // Configure one-to-many relationship between Event and Feedback
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Event)
                .WithMany(e => e.Feedbacks)
                .HasForeignKey(f => f.EventId);
        }
    }
}