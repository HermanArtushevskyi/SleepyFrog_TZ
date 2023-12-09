using System.Collections.Generic;
using System.Linq;
using CodeBase.Common;
using CodeBase.EnemyBehaviour;
using CodeBase.Factories.Interfaces;
using CodeBase.GameFlow.GameTimer;
using UnityContext.Interfaces;
using UnityEngine;
using Zenject;
using EEnemyBehaviour = CodeBase.EnemyBehaviour.EnemyBehaviour;

namespace CodeBase.GameFlow.EnemySpawner
{
    public class EnemySpawner
    {
        private readonly GameSettings _settings;
        private readonly IGameObjectFactory<EEnemyBehaviour, ScriptableEnemy, Vector3> _enemyFactory;
        private readonly Timer _timer;
        private readonly IEnumerable<ScriptableEnemy> _enemies;
        private readonly Transform _rightSpawner;
        private readonly Transform _leftSpawner;

        private float _spawnDelay;
        private float _timeFromLastSpawn;

        public EnemySpawner(
            GameSettings settings,
            IGameObjectFactory<EEnemyBehaviour, ScriptableEnemy, Vector3> enemyFactory,
            IUpdateCallback updateCallback,
            Timer timer,
            IEnumerable<ScriptableEnemy> enemies,
            [InjectOptional(Id = SceneObjectId.RightEnemySpawner)] Transform rightSpawner,
            [InjectOptional(Id = SceneObjectId.LeftEnemySpawner)] Transform leftSpawner)
        {
            _settings = settings;
            _enemyFactory = enemyFactory;
            updateCallback.OnUpdate += OnUpdate;
            _timer = timer;
            _enemies = enemies;
            _rightSpawner = rightSpawner;
            _leftSpawner = leftSpawner;
            _timeFromLastSpawn = 0;
            _spawnDelay = float.MaxValue;
        }

        private void OnUpdate()
        {
            _timeFromLastSpawn += Time.deltaTime;
            CalculateSpawnTime();
            TryToSpawn();
        }

        private void CalculateSpawnTime()
        {
            float spawnAmountPerSecond = _settings.EnemySpawnCurve.Evaluate(_timer.Seconds);
            _spawnDelay = 1 / spawnAmountPerSecond;
        }

        private void TryToSpawn()
        {
            if (_timeFromLastSpawn < _spawnDelay)
                return;
            
            _timeFromLastSpawn = 0;
            Spawn();
        }

        private void Spawn()
        {
            int enemyIndex = Random.Range(0, _enemies.Count());
            ScriptableEnemy enemy = _enemies.ElementAt(enemyIndex);
            Vector3 spawnPosition = Random.Range(0, 2) == 0 ? _rightSpawner.position : _leftSpawner.position;
            _enemyFactory.Create(enemy, spawnPosition);
        }
    }
}