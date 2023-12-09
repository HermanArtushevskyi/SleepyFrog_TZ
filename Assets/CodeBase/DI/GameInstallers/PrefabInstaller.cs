using System.Collections.Generic;
using CodeBase.Common;
using CodeBase.EnemyBehaviour;
using UnityEngine;
using Zenject;

namespace CodeBase.DI.GameInstallers
{
    public class PrefabInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject cameraPrefab;
        [SerializeField] private ScriptableEnemy[] enemiesPrefabs;
        
        public override void InstallBindings()
        {
            Container.Bind<GameObject>().WithId(PrefabId.Player).FromInstance(playerPrefab).AsCached();
            Container.Bind<GameObject>().WithId(PrefabId.Camera).FromInstance(cameraPrefab).AsCached();
            Container.Bind<IEnumerable<ScriptableEnemy>>().WithId(PrefabId.Enemy).FromInstance(enemiesPrefabs).AsCached();
        }
    }
}