using System.Threading.Tasks;

namespace StateMachineBot
{
    public interface IState
    {
        Task<MessageEventResult> Update(MessageEvent data);
    }
}