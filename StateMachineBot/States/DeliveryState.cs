using System.Threading.Tasks;

namespace StateMachineBot.States
{
    public class DeliveryState: IState
    {
        private readonly IStateMachine _stateMachine;

        public DeliveryState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async Task<MessageEventResult> Update(MessageEvent data)
        {
            if (data.Message != "Адрес1")
                return "Введите адрес";
            
            _stateMachine.SetState(data.Id, new DeliveryState(_stateMachine));
            return "Заказ отправлен";
        }
    }
}