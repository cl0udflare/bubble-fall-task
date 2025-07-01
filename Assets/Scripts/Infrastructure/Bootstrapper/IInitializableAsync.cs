using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Bootstrapper
{
    public interface IInitializableAsync
    {
        UniTask InitializeAsync(CancellationToken cancellationToken = default);
    }
}