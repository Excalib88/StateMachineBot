using System;
using System.Threading.Tasks;

namespace StateMachineBot.States
{
    class InitState : IState
    {
        private readonly IStateMachine _stateMachine;

        public InitState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async Task<MessageEventResult> Update(MessageEvent data)
        {
            if (data.Message != "Заказ")
                return "Команда не найдена! Повторите попытку...";
            
            _stateMachine.SetState(data.Id, new ProcessState(_stateMachine));

            return "Выберите товар";
        }
    }
}