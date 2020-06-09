using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace StateMachineBot
{
    public class OrderService
    {
        private Dictionary<long, OrderState> _ordersCollection;

        public OrderService()
        {
            _ordersCollection = new Dictionary<long, OrderState>();
        }

        private void GetOrInitState(long userId)
        {
            if (_ordersCollection.ContainsKey(userId)) return;
            
            _ordersCollection.Add(userId, new OrderState());
        }

        public string ProcessRequest(long userId)
        {
            GetOrInitState(userId);
            
            return "Заявка в обработке";
        }

        public string PayRequest(long userId)
        {
            GetOrInitState(userId);
            
            return "Заказ оплачен";
        }

        public string DeliveryRequest(long userId)
        {
            GetOrInitState(userId);
            
            return "Заказ отправлен";
        }

        public string RecieveRequest(long userId)
        {
            GetOrInitState(userId);
            
            return "Заказ получен";
        }
    }
}