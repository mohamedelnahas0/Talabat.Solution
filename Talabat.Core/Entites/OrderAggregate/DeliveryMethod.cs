using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortname, string description, string deliveryTime, decimal cost)
        {
            Shortname = shortname;
            Description = description;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public string Shortname { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
