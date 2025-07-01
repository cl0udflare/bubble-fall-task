using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.States.GameStates;
using Logging;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        private readonly DiContainer _container;

        public GameStateMachine(DiContainer container) =>
            _container = container;

        public UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            RegisterStates();

            DebugLogger.LogMessage(message: $"Initialized", sender: this);
            return UniTask.CompletedTask;
        }

        public void Enter<TState>() where TState : class, IState
        {
            if (_states.TryGetValue(typeof(TState), out IState state))
            {
                DebugLogger.LogMessage(message: $"Entering state: {typeof(TState).Name}", sender: this);

                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
            else
            {
                DebugLogger.LogError(message: $"State {typeof(TState).Name} not registered in state machine!",
                    sender: this);
            }
        }

        private void RegisterStates()
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(GameplayState)] = _container.Resolve<GameplayState>()
            };
        }
    }
}