using CodeBase.UI.Game;
using Zenject;

namespace CodeBase.Factories
{
    public class GameUIFactory : Interfaces.IFactory<GamePresenter>
    {
        private readonly DiContainer _container;

        public GameUIFactory(DiContainer container)
        {
            _container = container;
        }

        public GamePresenter Create()
        {
            GamePresenter presenter = _container.Instantiate<GamePresenter>();
            _container.Bind<GamePresenter>().FromInstance(presenter).AsSingle();
            return presenter;
        }
    }
}