using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string byuerEmail, string BasketId, int DeliveryMethodId, Address ShippingAdress);
        Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string byuerEmail);

        Task<Order> GetOrderByIdForSpecificUserAsync(string byuerEmail, int OrderId);


    }
}
