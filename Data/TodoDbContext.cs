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
        public DbSet<Category> Categories { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
           .HasMany(c => c.Todos)
           .WithOne(t => t.Category)
           .HasForeignKey(t => t.CategoryId);

            builder.Entity<Todo>().HasData(
                new Todo
                {
                    Id = 1,
                    Title = "Schedule dentist appointment",
                    Description = "Call the clinic and book a time for the dental check-up",
                    IsCompleted = false,
                    CategoryId = 1 // Correspond à la catégorie "Health"
                },
                new Todo
                {
                    Id = 2,Title = "Plan weekend hike",
                    Description = "Research trails and prepare gear for a Saturday hike",
                    IsCompleted = true,
                    CategoryId = 2 // Correspond à la catégorie "Leisure"
                }
              );

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Health" },
                new Category { Id = 2, Name = "Leisure" },
                 new Category { Id = 3, Name = "Bil" }


            );

        }
    }

        
    }
