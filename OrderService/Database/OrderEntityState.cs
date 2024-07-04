using MassTransit;

namespace OrderService.Database
{
    public class OrderEntityState : SagaStateMachineInstance
    {
        public Guid CorrelationId{ get; set; }
        public string CurrentState { get; set; }
        public Guid OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public bool PaymentStatus { get; set; }
        public bool InventoryStatus { get; set; }
    }
}
