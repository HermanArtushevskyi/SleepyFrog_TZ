using System;
using CodeBase.Audio;
using CodeBase.GameFlow.EnemySpawner;
using CodeBase.GameFlow.GameTimer;
using CodeBase.GameFlow.HealthCounter;
using CodeBase.GameFlow.KillCounter;
using CodeBase.GameFlow.ResultManager;
using Zenject;

namespace CodeBase.DI.GameInstallers
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Timer>().To<Timer>().AsSingle();
            Container.Bind<EnemySpawner>().To<EnemySpawner>().AsSingle();
            Container.Bind<HealthCounter>().To<HealthCounter>().AsSingle();
            Container.Bind<KillCounter>().To<KillCounter>().AsSingle();
            Container.Bind<ResultManager>().To<ResultManager>().AsSingle();
            Container.Bind<AudioPlayer>().To<AudioPlayer>().AsSingle();
        }

        private void OnDestroy()
        {
            Container.UnbindAll();
        }
    }
}