using Contract;
using Inventory.Service.API.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Service.API.Handlers
{
    public class CompensateInventoryHandler : IConsumer<CompensateInventory>
    {
        private readonly InventoryContext _context;

        public CompensateInventoryHandler(InventoryContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<CompensateInventory> context)
        {
            var inventoreEntity = await _context.inventories.FirstOrDefaultAsync(x =>
                x.id.Equals(context.Message.inventoryId));
            if (inventoreEntity != null)
            {
                inventoreEntity.unit = inventoreEntity.unit + context.Message.quantity;
                _context.inventories.Update(inventoreEntity);
                _context.SaveChanges();
            }

        }
    }
}
