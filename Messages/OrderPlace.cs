using System;
using NServiceBus;

namespace Messages
{
    public class OrderPlace : ICommand
    {
        public string OrderID { get; set; }
    }
}
