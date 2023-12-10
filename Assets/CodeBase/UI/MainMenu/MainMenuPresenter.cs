using CodeBase.DataSaver.Interfaces;
using CodeBase.GameFlow.ResultManager;
using CodeBase.SceneManagement.Common;
using CodeBase.SceneManagement.Interfaces;
using CodeBase.Settings;
using UnityEngine;

namespace CodeBase.UI.MainMenu
{
    public class MainMenuPresenter
    {
        private readonly MainMenuView _view;
        private readonly ISceneSwitcher _sceneSwitcher;
        private readonly SettingsStorage _settingsStorage;
        private readonly ILoader _loader;

        public MainMenuPresenter(MainMenuView view, ISceneSwitcher sceneSwitcher, SettingsStorage settingsStorage,
            ILoader loader)
        {
            _view = view;
            _sceneSwitcher = sceneSwitcher;
            _settingsStorage = settingsStorage;
            _loader = loader;
            SetSoundImage();
            SetHighScoreText();
            BindButtons();
        }

        private void BindButtons()
        {
            _view._playButton.onClick.AddListener(OnPlayButtonClicked);
            _view._exitButton.onClick.AddListener(OnExitButtonClicked);
            _view._soundButton.onClick.AddListener(OnSoundButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _sceneSwitcher.LoadScene(SceneNames.Game);
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void OnSoundButtonClicked()
        {
            _settingsStorage.SoundEnabled.Toggle();
            _settingsStorage.Save();
            SetSoundImage();
        }

        private void SetSoundImage()
        {
             _view._soundImage.sprite = _settingsStorage.SoundEnabled.Value ? _view._soundOnSprite : _view._soundOffSprite;
        }

        private void SetHighScoreText()
        {
            Result result = null;
            if (_loader.Load<Result>(out result))
                _view._highScoreText.text = $"High Score: {result.Kills}";
        }
    }
}