namespace StateMachineBot
{
    public class OrderState
    {
        public Order Order { get; set; }

        public OrderState()
        {
            Order = new Order
            {
                OrderStage = OrderStage.Processing
            };
            // пишем в базу
        }
        
        public void ProcessHandle()
        {
            Order.OrderStage = OrderStage.Paying;
            // пишем в базу
        }

        public void PaymentHandle()
        {
            Order.OrderStage = OrderStage.Delivering;
            // пишем в базу
        }

        public void Delivering()
        {
            Order.OrderStage = OrderStage.Received;
            // пишем в базу
        }
    }
}