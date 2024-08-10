using System;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Basket;
using Talabat.Core.Services;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Repositories.Contract;
using Talabat.Core;
using Talabat.Core.Entites.OrderAggregate;
using Talabat.Core.Entites;
using Product = Talabat.Core.Entites.Product;

namespace Talabat.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitofwork _unitofwork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitofwork unitofwork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitofwork = unitofwork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKeys:SecretKey"];
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null) return null;
            var shippingprice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _unitofwork.Repository<DeliveryMethod>().GetbyIdAsync(basket.DeliveryMethodId.Value);
                shippingprice = DeliveryMethod.Cost;
            }

            if (basket.Items.Count > 0)
            {
                foreach(var item in basket.Items)
                {
                    var Product = await _unitofwork.Repository<Product>().GetbyIdAsync(item.Id);
                    if(item.Price  != Product.Price) 
                    {
                       item.Price = Product.Price;
                    }
                    var Subtotal = basket.Items.Sum(item => item.Price * item.Quantity);

                    var Service = new PaymentIntentService();
                    PaymentIntent paymentIntent;
                    if(string.IsNullOrEmpty(basket.PaymentIntentId))
                    {
                        var Options = new PaymentIntentCreateOptions()
                        {
                            Amount = (long)(Subtotal * 100 + shippingprice * 100),
                            Currency = "usd",
                            PaymentMethodTypes=new List<string>() {"card"}
                        };
                        paymentIntent = await Service.CreateAsync(Options);
                        basket.PaymentIntentId = paymentIntent.Id;
                        basket.ClientSecret = paymentIntent.ClientSecret;
                    }
                    else
                    {
                        var Options = new PaymentIntentUpdateOptions()
                        {
                            Amount = (long)(Subtotal * 100 + shippingprice * 100),
                          
                        };
                       paymentIntent=  await Service.UpdateAsync(basket.PaymentIntentId, Options);
                        basket.PaymentIntentId = paymentIntent.Id;
                        basket.ClientSecret = paymentIntent.ClientSecret;
                    }

                }
            }
            await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
     
}