using CodeBase.Factories;
using CodeBase.UI.MainMenu;
using Zenject;
using IFactories = CodeBase.Factories.Interfaces;

namespace CodeBase.DI.MainMenuInstallers
{
    public class MainMenuFactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFactories.IFactory<MainMenuPresenter>>().To<MainMenuFactory>().AsSingle();
        }
        
        private void OnDestroy()
        {
            Container.UnbindAll();
        }
    }
}