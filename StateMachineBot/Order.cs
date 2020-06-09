using System;

namespace StateMachineBot
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductId { get; set; } 
        public OrderStage OrderStage { get; set; }
    }
}