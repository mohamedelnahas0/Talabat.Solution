using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites.Basket;

namespace Talabat.APIs.DTOS
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } 

        [Required]
        public List<BasketItemDto> Items { get; set; } 
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }


    }
}
