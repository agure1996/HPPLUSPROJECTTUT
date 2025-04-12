using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HPlusProject.API.Models
{
    public class Product
    {
        
        public int Id { get; set; }

        [Required]
        public string Sku { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [JsonIgnore] //dont want the category object to be provided when I serialize this object, so I label it with JSONIgnore
        public virtual Category? Category { get; set; }
    }
}
