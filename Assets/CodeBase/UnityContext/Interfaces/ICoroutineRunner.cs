using System.Collections;

namespace UnityContext.Interfaces
{
    public interface ICoroutineRunner
    {
        public void RunCoroutine(IEnumerator coroutine);
    }
}