using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services;
using Talabat.Core.Specifications.OrderSpecs;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;

        private readonly IUnitofwork _unitofwork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository ,
            IUnitofwork unitofwork,
            IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitofwork = unitofwork;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string byuerEmail, string BasketId, int DeliveryMethodId, Address ShippingAdress)
        {
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            var orderItems = new List<OrderItem>();
         
            if (Basket?.Items.Count>0)
            {
                foreach(var item in Basket.Items)
                {
                    var Product = await _unitofwork.Repository<Product>().GetbyIdAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrderd(Product.Id , Product.Name , Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrdered, item.Quantity , Product.Price);
                    orderItems.Add(OrderItem);
                }
            }
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);
             var DeliveryMethod =  await _unitofwork.Repository<DeliveryMethod>().GetbyIdAsync(DeliveryMethodId);
            var specs = new OrderSWithPaymnetSpecs(Basket.PaymentIntentId);
            var ExOrder = await _unitofwork.Repository<Order>().GetbyEntitywithspecsAsync(specs);
            if(ExOrder is not null)
            {
                _unitofwork.Repository<Order>().Delete(ExOrder);
                _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            }
            var Order = new Order(byuerEmail, ShippingAdress, DeliveryMethod, orderItems, subtotal , Basket.PaymentIntentId);
           await _unitofwork.Repository<Order>().Add(Order);
             var Result = await _unitofwork.CompleteAsync();
            if (Result <= 0) return null;
            return Order;

        }

        public async Task<Order> GetOrderByIdForSpecificUserAsync(string byuerEmail, int OrderId)
        {
            var Specs = new OrderSpecifications(byuerEmail , OrderId);

            var Orders = await _unitofwork.Repository<Order>().GetbyEntitywithspecsAsync(Specs);
            return Orders;

        }

        public Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string byuerEmail)
        {
            var Specs = new OrderSpecifications(byuerEmail);
            var Order = _unitofwork.Repository<Order>().GetAllwithspecsAsync(Specs);
            return Order;

        }
    }
}
