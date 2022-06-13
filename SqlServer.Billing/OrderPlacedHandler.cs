using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;
using Messages;
using System.Threading.Tasks;
using NServiceBus.Logging;

namespace SqlServer.Billing
{
    class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
        public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Order Billed with order ID = {message.OrderID}");
            await context.Publish(new OrderBilled() { OrderID = message.OrderID });
           
        }
    }
}
