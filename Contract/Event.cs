using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }
        public string InventoryId { get; set; }
        public int Quantity { get; set; }
    }
    public class InventoryOrderedSuccess
    {
        public Guid OrderId { get; set; }
    }
    public class InventoryOrderedFailed
    {
        public Guid OrderId { get; set; }
    }
    public class PaymentSuccess
    {
        public Guid OrderId { get; set; }
    }
    public class PaymentFailed
    {
        public Guid OrderId { get; set; }
    }
}
