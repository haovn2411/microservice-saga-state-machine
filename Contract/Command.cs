using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public record UpdateUnitInventory
    {
        public Guid orderId { get; set; }
        public string inventoryId { get; set; }

        public int quantity { get; set; }
    }

    public record CompensateInventory
    {
        public Guid orderId { get; set; }
        public string inventoryId { get; set; }
        public int quantity { get; set; }
    }

}
