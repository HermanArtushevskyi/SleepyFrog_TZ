using System.Collections.Generic;
using CodeBase.Common;
using CodeBase.EnemyBehaviour;
using CodeBase.GameFlow.KillCounter;
using CodeBase.ObjectPooling;
using UnityEngine;
using Zenject;
using EEnemyBehaviour = CodeBase.EnemyBehaviour.EnemyBehaviour;
using IFactories = CodeBase.Factories.Interfaces;

namespace CodeBase.Factories
{
    public class EnemyFactory : IFactories.IGameObjectFactory<EEnemyBehaviour, ScriptableEnemy, Vector3>
    {
        private readonly DiContainer _container;
        private readonly Dictionary<string, IPool<GameObject>> _pools = new();
        private readonly IFactories.IFactory<GameObject, GameObject> _gameObjectFactory;
        private readonly KillCounter _killCounter;

        public EnemyFactory(
            DiContainer container,
            IFactories.IFactory<GameObject, GameObject> gameObjectFactory,
            KillCounter killCounter)
        {
            _container = container;
            _gameObjectFactory = gameObjectFactory;
            _killCounter = killCounter;
        }

        public EEnemyBehaviour Create(ScriptableEnemy enemy, Vector3 position)
        {
            if (!_pools.ContainsKey(enemy.Prefab.name))
                AddPool(enemy.Prefab, 30);
            
            GameObjectPool pool = (GameObjectPool)_pools[enemy.Prefab.name];
            GameObject instance = pool.Get();
            instance.transform.position = position;
            EEnemyBehaviour enemyBehaviour = instance.GetComponent<EEnemyBehaviour>();
            enemyBehaviour.SetStats(enemy.Stats);
            enemyBehaviour.OnDeath += OnEnemyDeath;
            instance.SetActive(true);
            return enemyBehaviour;
        }

        public void AddPool(GameObject prefab, int initialSize)
        {
            if (_pools.ContainsKey(prefab.name))
                return;
            
            _pools.Add(prefab.name, new GameObjectPool(prefab, initialSize, _gameObjectFactory));
        }

        private void OnEnemyDeath(EEnemyBehaviour obj)
        {
            _killCounter.CurrentKills++;
            obj.OnDeath -= OnEnemyDeath;
            _pools[obj.gameObject.name].Return(obj.gameObject);
        }
    }
}