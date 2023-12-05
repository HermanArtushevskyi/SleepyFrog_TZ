using CodeBase.SceneManagement;
using CodeBase.SceneManagement.Interfaces;
using UnityContext;
using UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.DI.GlobalInstallers
{
    public class GlobalDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private MonoContext _monoContext;
        
        public override void InstallBindings()
        {
            BindUnityContext();
            BindSceneLoader();
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
    }
}