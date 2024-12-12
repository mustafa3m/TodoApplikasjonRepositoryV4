using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApplikasjonAPIEntityDelTre.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [JsonIgnore] // Ignore this property during serialization
        public ICollection<Todo>? Todos { get; set; }
    }
}
