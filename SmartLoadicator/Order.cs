using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLoadicator
{
    public class Order
    {
        public int OrderId  {get; set; }
        public string OrderName { get; set; }
        public int ItemId { get; set; }
        public int CustomerId { get; set; }
    }
}
