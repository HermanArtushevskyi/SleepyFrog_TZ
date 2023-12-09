using CodeBase.Common;
using UnityEngine;
using Zenject;

namespace CodeBase.DI.GameInstallers
{
    public class SceneObjectsInstaller : MonoInstaller
    {
        [SerializeField] private Transform spawnpoint;
        [SerializeField] private Transform leftEnemySpawner;
        [SerializeField] private Transform rightEnemySpawner;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(SceneObjectId.Spawnpoint).FromInstance(spawnpoint).AsCached();
            Container.Bind<Transform>().WithId(SceneObjectId.LeftEnemySpawner).FromInstance(leftEnemySpawner).AsCached();
            Container.Bind<Transform>().WithId(SceneObjectId.RightEnemySpawner).FromInstance(rightEnemySpawner).AsCached();
        }
    }
}