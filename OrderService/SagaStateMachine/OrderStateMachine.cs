using Contract;
using MassTransit;
using OrderService.Database;

namespace Order.Service.API.SagaStateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderEntityState>
    {
        public State InventoryChecking { get; set; }
        public State InventoryFailure { get; set; }
        public State PaymentProcessing { get; set; }
        public State PaymentSuccess { get; set; }
        public State PaymentFailure { get; set; }
        

        public Event<OrderCreated> OrderCreated { get; set; }
        public Event<InventoryOrderedSuccess> InventoryOrderedSuccess { get; set; }
        public Event<InventoryOrderedFailed> InventoryOrderedFailed { get;  set; }
        public Event<PaymentSuccess> PaymentSucceed { get; set; }
        public Event<PaymentFailed> PaymentFailed { get; set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);
            Event(() => OrderCreated, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => InventoryOrderedSuccess, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => InventoryOrderedFailed, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => PaymentSucceed, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => PaymentFailed, x => x.CorrelateById(context => context.Message.OrderId));

            Initially(
                When(OrderCreated)
                    .Then(context =>
                    {
                        context.Saga.OrderId = context.Message.OrderId;
                        context.Saga.ProductId = context.Message.InventoryId;
                        context.Saga.Quantity = context.Message.Quantity;
                    })
                    .TransitionTo(InventoryChecking)
                    .Publish(context => new UpdateUnitInventory
                    {
                        inventoryId = context.Message.InventoryId,
                        orderId = context.Message.OrderId,
                        quantity = context.Message.Quantity,
                    }));

            During(InventoryChecking,
                When(InventoryOrderedSuccess)
                    .Then(context =>
                    {
                        context.Saga.InventoryStatus = true;
                    })
                    .TransitionTo(PaymentProcessing),
                When(InventoryOrderedFailed)
                    .Then(context =>
                    {
                        context.Saga.InventoryStatus = false;
                    })
                    .TransitionTo(InventoryFailure));

            During(PaymentProcessing,
                When(PaymentSucceed)
                .Then(context =>
                {
                    context.Saga.PaymentStatus = true;
                })
                .TransitionTo(PaymentSuccess),
                When(PaymentFailed)
                .Then(context =>
                {
                    context.Saga.PaymentStatus = false;
                })
                .TransitionTo(PaymentFailure)
                .Publish(context => new CompensateInventory
                {
                    inventoryId = context.Saga.ProductId,
                    orderId = context.Saga.OrderId,
                    quantity = context.Saga.Quantity,
                }));
            SetCompletedWhenFinalized();
        }

    }
}
