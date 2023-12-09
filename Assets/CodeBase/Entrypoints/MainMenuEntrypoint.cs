using System;
using CodeBase.UI.MainMenu;
using UnityEngine;
using Zenject;
using IFactories = CodeBase.Factories.Interfaces;

namespace CodeBase.Entrypoints
{
    public class MainMenuEntrypoint : MonoBehaviour
    {
        private IFactories.IFactory<MainMenuPresenter> _uiFactory;

        [Inject]
        private void Construct(IFactories.IFactory<MainMenuPresenter> uiFactory)
        {
            _uiFactory = uiFactory;
        }

        private void Start()
        {
            _uiFactory.Create();
        }
    }
}