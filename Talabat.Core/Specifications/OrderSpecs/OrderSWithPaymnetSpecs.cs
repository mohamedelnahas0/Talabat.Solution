using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderSWithPaymnetSpecs : BaseSpecifications<Order>
    {
        public OrderSWithPaymnetSpecs(string PaymentIntentId) :base(O => O.PaymentintentId==PaymentIntentId)
        {
            
        }
    }
}
