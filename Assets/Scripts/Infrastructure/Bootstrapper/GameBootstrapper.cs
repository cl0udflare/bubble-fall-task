using System.Threading;
using Infrastructure.Services.SceneTransition;
using Infrastructure.Services.Systems;
using Infrastructure.States;
using Infrastructure.States.GameStates;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrapper
{
    public class GameBootstrapper : IInitializable
    {
        private const float MAX_SERVICE_PROGRESS = 0.3f;
        
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneTransitionService _sceneTransition;
        private readonly InitializationManagement _initManagement;

        public GameBootstrapper(IGameStateMachine stateMachine, ISceneTransitionService sceneTransition, ISystemFactory systemFactory)
        {
            _stateMachine = stateMachine;
            _sceneTransition = sceneTransition;
            _initManagement = systemFactory.Create<InitializationManagement>();
        }

        public async void Initialize()
        {
            using CancellationTokenSource token = new CancellationTokenSource();

            await _initManagement.InitializeAsync(
                onProgress: p => _sceneTransition.ReportInitializationProgress(p * MAX_SERVICE_PROGRESS),
                token: token.Token);

            Application.targetFrameRate = 60;
            _stateMachine.Enter<GameplayState>();
        }
    }
}