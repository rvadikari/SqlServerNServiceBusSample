using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;
using Messages;
using System.Threading.Tasks;
using NServiceBus.Logging;

namespace SqlServer.Sales
{
    class OrderPlaceHandler : IHandleMessages<OrderPlace>
    {
        static ILog log = LogManager.GetLogger<OrderPlaceHandler>();
        public async Task Handle(OrderPlace message, IMessageHandlerContext context)
        {
            log.Info($"Order Placed with OrderID = {message.OrderID}");
            await context.Publish(new OrderPlaced() { OrderID = message.OrderID });
        }
    }
}
