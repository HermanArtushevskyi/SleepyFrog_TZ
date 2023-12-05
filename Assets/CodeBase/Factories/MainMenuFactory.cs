using CodeBase.UI.MainMenu;
using Zenject;

namespace CodeBase.Factories
{
    public class MainMenuFactory : Interfaces.IFactory<MainMenuPresenter>
    {
        private readonly DiContainer _container;

        public MainMenuFactory(DiContainer container)
        {
            _container = container;
        }

        public MainMenuPresenter Create()
        {
            MainMenuPresenter presenter = _container.Instantiate<MainMenuPresenter>();
            _container.Bind<MainMenuPresenter>().FromInstance(presenter).AsSingle();
            return presenter;
        }
    }
}