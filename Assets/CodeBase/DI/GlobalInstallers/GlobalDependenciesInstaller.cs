using CodeBase.DataSaver;
using CodeBase.DataSaver.Common;
using CodeBase.DataSaver.Interfaces;
using CodeBase.Input;
using CodeBase.Input.Interfaces;
using CodeBase.SceneManagement;
using CodeBase.SceneManagement.Interfaces;
using CodeBase.Settings;
using UnityContext;
using UnityContext.Interfaces;
using UnityEngine;
using UnityInput;
using Zenject;

namespace CodeBase.DI.GlobalInstallers
{
    public class GlobalDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private MonoContext _monoContext;
        [SerializeField] private SaveSettings _saveSettings;
        
        public override void InstallBindings()
        {
            BindUnityContext();
            BindSceneLoader();
            BindSaveSettings();
            BindDataSaver();
            BindSettings();
            BindInput();
        }

        private void BindUnityContext() =>
            Container.Bind(typeof(ICoroutineRunner),
                typeof(IFixedUpdateCallback),
                typeof(IUpdateCallback))
                .FromInstance(_monoContext).AsSingle();

        private void BindSceneLoader() =>
            Container.Bind(typeof(ISceneSwitcher),
            typeof(ISceneEventsInvoker))
            .To<SceneLoader>().AsSingle();

        private void BindSaveSettings() =>
            Container.Bind<SaveSettings>().FromInstance(Instantiate(_saveSettings)).AsSingle();

        private void BindDataSaver() =>
            Container.Bind(typeof(ISaver), typeof(ILoader))
            .To<UniversalSaver>().AsSingle();

        private void BindSettings() => Container.Bind<SettingsStorage>().To<SettingsStorage>().AsSingle();
        
        private void BindInput() 
        {
            InputProvider inputProvider = new InputProvider();
            inputProvider.AddSource(new InputClearer());
            InputActions inputActions = new InputActions();
            inputActions.Enable();
            inputProvider.AddSource(new InputReader(inputActions));
            Container.Bind<IInputProvider>().FromInstance(inputProvider).AsSingle();
        }
    }
}