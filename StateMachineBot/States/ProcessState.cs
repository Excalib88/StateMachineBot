using System.Threading.Tasks;

namespace StateMachineBot.States
{
    public class ProcessState: IState
    {
        private readonly IStateMachine _stateMachine;

        public ProcessState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async Task<MessageEventResult> Update(MessageEvent data)
        {
            if (data.Message != "Товар1")
                return "Товар не найден";
            
            _stateMachine.SetState(data.Id, new PaymentState(_stateMachine));
            return "Товар выбран";
        }
    }
}