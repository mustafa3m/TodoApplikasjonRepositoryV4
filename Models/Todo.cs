using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


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
    }


}
