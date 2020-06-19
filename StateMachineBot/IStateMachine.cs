using System.Threading.Tasks;

namespace StateMachineBot
{
    public interface IStateMachine
    {
        Task<MessageEventResult> FireEvent(MessageEvent data);
        void SetState(string id, IState state);
    }
}