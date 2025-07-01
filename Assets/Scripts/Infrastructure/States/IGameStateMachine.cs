using Infrastructure.Bootstrapper;
using Infrastructure.States.GameStates;

namespace Infrastructure.States
{
    public interface IGameStateMachine : IInitializableAsync
    {
        void Enter<TState>() where TState : class, IState;
    }
}