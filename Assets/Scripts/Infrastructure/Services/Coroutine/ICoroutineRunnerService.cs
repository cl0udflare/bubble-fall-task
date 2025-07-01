using System.Collections;

namespace Infrastructure.Services.Coroutine
{
    public interface ICoroutineRunnerService
    {
        UnityEngine.Coroutine StartCoroutine(IEnumerator routine);
    }
}