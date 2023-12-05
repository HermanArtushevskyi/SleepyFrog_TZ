using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.SceneManagement.Interfaces
{
    public interface ISceneSwitcher
    {
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
        public AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
    }
}