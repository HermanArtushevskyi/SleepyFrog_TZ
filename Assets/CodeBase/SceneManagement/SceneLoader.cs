using System;
using System.Collections;
using CodeBase.SceneManagement.Interfaces;
using UnityContext.Interfaces;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

namespace CodeBase.SceneManagement
{
    public class SceneLoader : ISceneSwitcher, ISceneEventsInvoker
    {
        public event Action<string, LoadSceneMode> OnSceneLoadingStarted;
        public event Action<string, LoadSceneMode> OnSceneLoadingFinished;
        public event Action<string, float> OnSceneLoadProgress;

        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            OnSceneLoadingStarted?.Invoke(sceneName, mode);
            SceneManager.LoadScene(sceneName, mode);
            OnSceneLoadingFinished?.Invoke(sceneName, mode);
        }

        public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
            _coroutineRunner.RunCoroutine(LoadingCoroutine(sceneName, mode, operation));
            return operation;
        }

        private IEnumerator LoadingCoroutine(string sceneName, LoadSceneMode mode, AsyncOperation operation)
        {
            OnSceneLoadingStarted?.Invoke(sceneName, mode);
            while (!operation.isDone)
            {
                OnSceneLoadProgress?.Invoke(sceneName, operation.progress);
                yield return null;
            }
            OnSceneLoadingFinished?.Invoke(sceneName, mode);
        }
    }
}