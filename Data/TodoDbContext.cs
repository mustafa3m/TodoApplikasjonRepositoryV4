using Microsoft.EntityFrameworkCore;
using TodoApplikasjonAPIEntityDelTre.Models;

namespace TodoApplikasjonAPIEntityDelTre.Data
{
    public class TodoDbContext : DbContext
    {


        // Create a constructor
        public TodoDbContext(DbContextOptions options) : base(options) { } // Constructor initializes the DbContext with options.

        // Represents the Todos table in the database.
        public DbSet<Todo> Todos { get; set; } 




        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seed data added to the Users table when the database is created.
            builder.Entity<Todo>().HasData(
                 new Todo { Id = 1, Title = "Schedule dentist appointment", Description = "Call the clinic and book a time for the dental check-up", IsCompleted = false },
                 new Todo { Id = 2, Title = "Plan weekend hike", Description = "Research trails and prepare gear for a Saturday hike", IsCompleted = true }

             );

        }
    }

        
    }
