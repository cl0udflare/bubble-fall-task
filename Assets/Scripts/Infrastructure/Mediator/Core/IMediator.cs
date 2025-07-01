using Cysharp.Threading.Tasks;
using Infrastructure.Bootstrapper;
using Infrastructure.Mediator.Events;
using Infrastructure.Mediator.Requests;

namespace Infrastructure.Mediator.Core
{
    public interface IMediator : IInitializableAsync
    {
        UniTask Send<TRequest>(TRequest request) where TRequest : IRequest;
        UniTask<TResponse> Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
        UniTask Publish<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}