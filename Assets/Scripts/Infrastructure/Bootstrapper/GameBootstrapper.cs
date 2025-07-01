using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.States;
using Infrastructure.States.GameStates;
using Zenject;

namespace Infrastructure.Bootstrapper
{
    public class GameBootstrapper : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly List<IInitializableAsync> _initializableServices;

        public GameBootstrapper(IGameStateMachine stateMachine, List<IInitializableAsync> initializableServices)
        {
            _stateMachine = stateMachine;
            _initializableServices = initializableServices;
        }

        public async void Initialize()
        {
            await InitializeServices();
            
            _stateMachine.Enter<GameplayState>();
        }
        
        private async UniTask InitializeServices()
        {
            foreach (IInitializableAsync service in _initializableServices) 
                await service.InitializeAsync();
        }
    }
}