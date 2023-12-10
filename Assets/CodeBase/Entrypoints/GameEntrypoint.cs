using CodeBase.Common;
using CodeBase.EnemyBehaviour;
using CodeBase.GameFlow.EnemySpawner;
using CodeBase.GameFlow.GameTimer;
using CodeBase.UI.Game;
using UnityEngine;
using Zenject;
using IFactories = CodeBase.Factories.Interfaces;
using CCharacterController = CodeBase.CharacterController.CharacterController;
using EEnemyBehaviour = CodeBase.EnemyBehaviour.EnemyBehaviour;

namespace CodeBase.Entrypoints
{
    public class GameEntrypoint : MonoBehaviour
    {
        [SerializeField] private int _enemiesPreloadCount = 10;
        
        private IFactories.IFactory<CCharacterController> _playerFactory;
        private IFactories.IFactory<Camera, GameObject> _cameraFactory;
        private IFactories.IFactory<GamePresenter> _uiFactory;
        private IFactories.IGameObjectFactory<EEnemyBehaviour, ScriptableEnemy, Vector3> _enemyFactory;
        private ScriptableEnemy[] _enemiesPrefabs;
        private Timer _timer;
        private EnemySpawner _enemySpawner;

        [Inject]
        private void Construct(
            IFactories.IFactory<CCharacterController> playerFactory,
            IFactories.IFactory<Camera, GameObject> cameraFactory,
            IFactories.IFactory<GamePresenter> uiFactory,
            IFactories.IFactory<EEnemyBehaviour, ScriptableEnemy, Vector3> enemyFactory, 
            [InjectOptional(Id = PrefabId.Enemy)] ScriptableEnemy[] enemiesPrefabs,
            Timer timer,
            EnemySpawner enemySpawner)
        {
            _playerFactory = playerFactory;
            _cameraFactory = cameraFactory;
            _uiFactory = uiFactory;
            _enemyFactory = enemyFactory as IFactories.IGameObjectFactory<EEnemyBehaviour, ScriptableEnemy, Vector3>;
            _enemiesPrefabs = enemiesPrefabs;
            _timer = timer;
            _enemySpawner = enemySpawner;
        }
        
        private void Start()
        {
            CCharacterController player = _playerFactory.Create();
            _cameraFactory.Create(player.gameObject);
            _uiFactory.Create();
            AddPoolsToEnemyFactory();
            _enemySpawner.Start();
            _timer.Start();
        }

        private void AddPoolsToEnemyFactory()
        {
            foreach (ScriptableEnemy scriptableEnemy in _enemiesPrefabs)
            {
                _enemyFactory.AddPool(scriptableEnemy.Prefab, _enemiesPreloadCount);
            }
        }
    }
}