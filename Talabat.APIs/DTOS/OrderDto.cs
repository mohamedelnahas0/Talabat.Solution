using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.APIs.DTOS
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }
        }
    
}
