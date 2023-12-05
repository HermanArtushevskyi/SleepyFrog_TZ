using System;
using UnityEngine.SceneManagement;

namespace CodeBase.SceneManagement.Interfaces
{
    public interface ISceneEventsInvoker
    {
        public event Action<string, LoadSceneMode> OnSceneLoadingStarted;
        public event Action<string, LoadSceneMode> OnSceneLoadingFinished;
        public event Action<string, float> OnSceneLoadProgress;
    }
}