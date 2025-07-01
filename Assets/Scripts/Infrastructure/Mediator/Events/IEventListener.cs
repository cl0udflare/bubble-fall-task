using Cysharp.Threading.Tasks;

namespace Infrastructure.Mediator.Events
{
    public interface IEventListener<in TEvent> : IEventListenerMarker where TEvent : IEvent
    {
        UniTask OnEvent(TEvent evt);
    }
}