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
        private readonly IFactory<EEnemyBehaviour, ScriptableEnemy, Vector3> _enemyFactory;
        private readonly Timer _timer;
        private readonly IEnumerable<ScriptableEnemy> _enemies;
        private readonly Transform _rightSpawner;
        private readonly Transform _leftSpawner;

        private float _spawnDelay;
        private float _timeFromLastSpawn;
        private bool _isStarted;
        
        public EnemySpawner(
            GameSettings settings,
            IFactory<EEnemyBehaviour, ScriptableEnemy, Vector3> enemyFactory,
            IUpdateCallback updateCallback,
            Timer timer,
            [InjectOptional(Id = PrefabId.Enemy)] IEnumerable<ScriptableEnemy> enemies,
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
            _isStarted = false;
        }
        
        public void Start()
        {
            _isStarted = true;
        }

        private void OnUpdate()
        {
            if (!_isStarted)
                return;
            
            _timeFromLastSpawn += Time.deltaTime;
            CalculateSpawnTime();
            TryToSpawn();
        }

        private void CalculateSpawnTime()
        {
            float spawnAmountPerSecond = _settings.EnemySpawnCurve.Evaluate(_timer.Seconds);
            _spawnDelay = 1/ spawnAmountPerSecond;
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
            if (_enemies == null || !_enemies.Any())
                return;
            int enemyIndex = Random.Range(0, _enemies.Count());
            ScriptableEnemy enemy = _enemies.ElementAt(enemyIndex);
            if (_rightSpawner == null || _leftSpawner == null)
                return;
            Vector3 spawnPosition = Random.Range(0, 2) == 0 ? _rightSpawner.position : _leftSpawner.position;
            _enemyFactory.Create(enemy, spawnPosition);
        }
    }
}