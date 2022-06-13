using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;
using Messages;
using System.Threading.Tasks;
using NServiceBus.Logging;

namespace SqlServer.Shipping
{
    class ShippingPolicy : IHandleMessages<OrderPlaced>, IHandleMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<ShippingPolicy>();
        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Order Placed recieved with Order ID = {message.OrderID}. Should we ship?");
            return Task.CompletedTask;
        }

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Order Billed recieved with Order ID = {message.OrderID}. Should we ship?");
            return Task.CompletedTask;
        }
    }
}
