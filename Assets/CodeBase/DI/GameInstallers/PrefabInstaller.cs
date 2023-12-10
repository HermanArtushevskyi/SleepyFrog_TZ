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
        [SerializeField] private GameObject healthPrefab;
        [SerializeField] private ScriptableEnemy[] enemiesPrefabs;
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameObject audioSourcePrefab;
        [SerializeField] private AudioClip swordSwingAudioClip;
        [SerializeField] private GameObject comboPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<GameObject>().WithId(PrefabId.Player).FromInstance(playerPrefab).AsCached();
            Container.Bind<GameObject>().WithId(PrefabId.Camera).FromInstance(cameraPrefab).AsCached();
            Container.Bind<GameObject>().WithId(PrefabId.Health).FromInstance(healthPrefab).AsCached();
            Container.Bind<IEnumerable<ScriptableEnemy>>().WithId(PrefabId.Enemy).FromInstance(enemiesPrefabs).AsCached();
            Container.Bind<GameSettings>().FromInstance(ScriptableObject.Instantiate(gameSettings)).AsCached();
            Container.Bind<GameObject>().WithId(PrefabId.AudioSource).FromInstance(audioSourcePrefab).AsCached();
            Container.Bind<AudioClip>().WithId(PrefabId.SwordSwingAudioClip).FromInstance(swordSwingAudioClip).AsCached();
            Container.Bind<GameObject>().WithId(PrefabId.Combo).FromInstance(comboPrefab).AsCached();
        }
        
        private void OnDestroy()
        {
            Container.UnbindAll();
        }
    }
}