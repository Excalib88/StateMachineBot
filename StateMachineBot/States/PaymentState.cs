using System;
using System.Threading.Tasks;

namespace StateMachineBot.States
{
    public class PaymentState: IState
    {
        private readonly IStateMachine _stateMachine;

        public PaymentState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async Task<MessageEventResult> Update(MessageEvent data)
        {
            if (data.Message != "Оплачено")
                return "Оплатите заказ!";
            
            _stateMachine.SetState(data.Id, new DeliveryState(_stateMachine));
            return "Заказ успешно оплачен";
        }
    }
}