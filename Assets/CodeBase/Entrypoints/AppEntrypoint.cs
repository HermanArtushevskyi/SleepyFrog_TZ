using CodeBase.SceneManagement.Common;
using CodeBase.SceneManagement.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.Entrypoints
{
    public class AppEntrypoint : MonoBehaviour
    {
        private ISceneSwitcher _sceneSwitcher;
        
        [Inject]
        private void Construct(ISceneSwitcher sceneSwitcher)
        {
            _sceneSwitcher = sceneSwitcher;
        }

        private void Start() => _sceneSwitcher.LoadScene(SceneNames.Menu);
    }
}