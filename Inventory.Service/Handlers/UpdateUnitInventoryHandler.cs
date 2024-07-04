using Contract;
using Inventory.Service.API.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Service.API.Handlers
{
    public class UpdateUnitInventoryHandler : IConsumer<UpdateUnitInventory>
    {
        private readonly InventoryContext _dbcontext;

        public UpdateUnitInventoryHandler(InventoryContext context)
        {
            _dbcontext = context;
        }
        public async Task Consume(ConsumeContext<UpdateUnitInventory> context)
        {
            var inventoryEntity = await _dbcontext.inventories.FirstOrDefaultAsync(x => x.id.Equals(context.Message.inventoryId));
            if (inventoryEntity == null) return;
            if (inventoryEntity.unit < context.Message.quantity)
            {
                await context.Publish(new InventoryOrderedFailed
                {
                    OrderId = context.Message.orderId,
                });
            }
            else
            {
                inventoryEntity.unit = inventoryEntity.unit - context.Message.quantity;
                _dbcontext.inventories.Update(inventoryEntity);
                _dbcontext.SaveChanges();
                await context.Publish(new InventoryOrderedSuccess
                {
                    OrderId = context.Message.orderId,
                });
            }
        }
    }
}
