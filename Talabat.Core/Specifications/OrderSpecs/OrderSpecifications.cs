using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.Core.Specifications.OrderSpecs
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string email) : base(O => O.BuyerEmail == email) 
        {
            Includes.Add(O => O.DeliveryMethod); 
            Includes.Add(O => O.Items);
            AddOrderByDesc(O => O.Orderdate); 
        }

        public OrderSpecifications(string email , int OrderId) : base(O => O.BuyerEmail == email &&O.Id == OrderId)
        {

            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
          
        }

    }
}
