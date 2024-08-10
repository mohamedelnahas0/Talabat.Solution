using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.APIs.DTOS
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset Orderdate { get; set; } 
        public string Status { get; set; } 
        public Address ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; } //Name

        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }
        public string PaymentintentId { get; set; } 
    }
}
