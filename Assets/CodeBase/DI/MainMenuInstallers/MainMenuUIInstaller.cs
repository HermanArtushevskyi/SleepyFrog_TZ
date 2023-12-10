using CodeBase.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace CodeBase.DI.MainMenuInstallers
{
    public class MainMenuUIInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _view;
        
        public override void InstallBindings()
        {
            Container.Bind<MainMenuView>().FromInstance(_view).AsSingle();
        }
        
        private void OnDestroy()
        {
            Container.UnbindAll();
        }
    }
}