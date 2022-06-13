using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Messages
{
    public class OrderBilled:IEvent
    {
        public string OrderID { get; set; }
    }
}
