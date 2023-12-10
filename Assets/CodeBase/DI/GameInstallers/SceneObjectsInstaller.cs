using CodeBase.Common;
using CodeBase.UI.Game;
using UnityEngine;
using Zenject;

namespace CodeBase.DI.GameInstallers
{
    public class SceneObjectsInstaller : MonoInstaller
    {
        [SerializeField] private Transform spawnpoint;
        [SerializeField] private Transform leftEnemySpawner;
        [SerializeField] private Transform rightEnemySpawner;
        [SerializeField] private GameView gameView;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(SceneObjectId.Spawnpoint).FromInstance(spawnpoint).AsCached();
            Container.Bind<Transform>().WithId(SceneObjectId.LeftEnemySpawner).FromInstance(leftEnemySpawner).AsCached();
            Container.Bind<Transform>().WithId(SceneObjectId.RightEnemySpawner).FromInstance(rightEnemySpawner).AsCached();
            Container.Bind<GameView>().FromInstance(gameView).AsSingle();
        }
        
        private void OnDestroy()
        {
            Container.UnbindAll();
        }
    }
}