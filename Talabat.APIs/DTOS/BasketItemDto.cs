using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites.Basket;

namespace Talabat.APIs.DTOS
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; } = null!;
        [Required]

        public string PictureUrl { get; set; } = null!;
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage ="Price Must be greater than Zero.")]

        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be  item at least.")]

        public int Quantity { get; set; }
        [Required]

        public string Category { get; set; } = null!;
        [Required]

        public string Brand { get; set; } = null!;
    }
}