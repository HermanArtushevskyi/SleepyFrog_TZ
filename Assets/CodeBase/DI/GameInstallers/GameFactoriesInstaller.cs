using CodeBase.EnemyBehaviour;
using CodeBase.Factories;
using CodeBase.GameFlow.EnemySpawner;
using CodeBase.GameFlow.HealthCounter;
using CodeBase.GameFlow.KillCounter;
using CodeBase.GameFlow.ResultManager;
using CodeBase.UI.Game;
using UnityEngine;
using Zenject;
using IFactories = CodeBase.Factories.Interfaces;
using CCharacterController = CodeBase.CharacterController.CharacterController;

namespace CodeBase.DI.GameInstallers
{
    public class GameFactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFactories.IFactory<GameObject, GameObject>>().To<GameObjectFactory>().AsSingle();
            Container.Bind<IFactories.IFactory<CCharacterController>>().To<PlayerFactory>().AsSingle();
            Container.Bind<IFactories.IFactory<Camera, GameObject>>().To<CameraFactory>().AsSingle();
            Container.Bind<IFactories.IFactory<EnemyBehaviour.EnemyBehaviour, ScriptableEnemy, Vector3>>()
                .To<EnemyFactory>().AsSingle();
            Container.Bind<IFactories.IFactory<GamePresenter>>().To<GameUIFactory>().AsSingle();
            Container.Bind<EnemySpawner>().To<EnemySpawner>().AsSingle();
            Container.Bind<HealthCounter>().To<HealthCounter>().AsSingle();
            Container.Bind<KillCounter>().To<KillCounter>().AsSingle();
            Container.Bind<ResultManager>().To<ResultManager>().AsSingle();
        }
    }
}