using CodeBase.SceneManagement.Common;
using CodeBase.SceneManagement.Interfaces;
using UnityEngine;

namespace CodeBase.UI.MainMenu
{
    public class MainMenuPresenter
    {
        private readonly MainMenuView _view;
        private readonly ISceneSwitcher _sceneSwitcher;

        public MainMenuPresenter(MainMenuView view, ISceneSwitcher sceneSwitcher)
        {
            _view = view;
            _sceneSwitcher = sceneSwitcher;
            BindButtons();
        }

        private void BindButtons()
        {
            _view._playButton.onClick.AddListener(OnPlayButtonClicked);
            _view._exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _sceneSwitcher.LoadScene(SceneNames.Game);
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}