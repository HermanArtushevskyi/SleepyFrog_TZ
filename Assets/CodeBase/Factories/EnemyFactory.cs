using System.Collections.Generic;
using CodeBase.Common;
using CodeBase.EnemyBehaviour;
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
        private readonly Dictionary<string, IPool<GameObject>> _pools = new Dictionary<string, IPool<GameObject>>();
        private readonly IFactories.IFactory<GameObject, GameObject> _gameObjectFactory;

        public EnemyFactory(DiContainer container, IFactories.IFactory<GameObject, GameObject> gameObjectFactory)
        {
            _container = container;
            _gameObjectFactory = gameObjectFactory;
        }

        public EEnemyBehaviour Create(ScriptableEnemy enemy, Vector3 position)
        {
            if (!_pools.ContainsKey(enemy.Prefab.name))
                AddPool(enemy.Prefab, 10);
            
            GameObjectPool pool = (GameObjectPool)_pools[enemy.Prefab.name];
            GameObject instance = pool.Get();
            instance.transform.position = position;
            EEnemyBehaviour enemyBehaviour = instance.GetComponent<EEnemyBehaviour>();
            enemyBehaviour.SetStats(enemy.Stats);
            instance.SetActive(true);
            return enemyBehaviour;
        }

        public void AddPool(GameObject prefab, int initialSize)
        {
            if (_pools.ContainsKey(prefab.name))
                return;
            
            _pools.Add(prefab.name, new GameObjectPool(prefab, initialSize, _gameObjectFactory));
        }
    }
}