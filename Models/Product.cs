using System.ComponentModel.DataAnnotations;

namespace GamingStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required, Range(1, 10000)]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}