using CodeBase.SceneManagement.Common;
using CodeBase.SceneManagement.Interfaces;
using CodeBase.Settings;
using UnityEngine;
using Zenject;

namespace CodeBase.Entrypoints
{
    public class AppEntrypoint : MonoBehaviour
    {
        private ISceneSwitcher _sceneSwitcher;
        private SettingsStorage _settingsStorage;

        [Inject]
        private void Construct(ISceneSwitcher sceneSwitcher, SettingsStorage settingsStorage)
        {
            _sceneSwitcher = sceneSwitcher;
            _settingsStorage = settingsStorage;
        }

        private void Start()
        {
            _settingsStorage.LoadFromMemory();
            _sceneSwitcher.LoadScene(SceneNames.Menu);
        }
    }
}