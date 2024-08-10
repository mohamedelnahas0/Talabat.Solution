using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem() { }
        public OrderItem(ProductItemOrderd product, int quantity, decimal price)
        {
            this.product = product;
            Quantity = quantity;
            Price = price;
        }

        public ProductItemOrderd product { get; set; }
        public int Quantity  { get; set; }
        public decimal Price { get; set; }
    }
}
