using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TodoApplikasjonAPIEntityDelTre.Models
{
    public class Todo
    {
        public int Id { get; set; }

        //
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; }

        // 
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Description must be at least 10 characters if provided.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "IsCompleted is required.")]
        public bool IsCompleted { get; set; } = false;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }// Foreign key
        //[System.Text.Json.Serialization.JsonIgnore] // Prevents serialization of the full Category object
        public Category? Category { get; set; }
    }


}
