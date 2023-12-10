using CodeBase.EnemyBehaviour;
using CodeBase.Factories;
using CodeBase.GameFlow.KillCounter;
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
            Container.Bind<IFactories.IFactory<ComboBehaviour, Vector3, int>>().To<ComboFactory>().AsSingle();
        }
        
        private void OnDestroy()
        {
            Container.UnbindAll();
        }
    }
}