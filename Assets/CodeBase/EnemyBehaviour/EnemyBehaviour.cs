using System;
using CodeBase.Common;
using CodeBase.GameFlow.HealthCounter;
using UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.EnemyBehaviour
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private HealthCounter _healthCounter;
        private GameObject _playerOnScene;
        private CharacterController.CharacterController _characterController;
        private float _speed;
        private int _damage;
        private int _health;

        public event Action<EnemyBehaviour> OnDeath;
        private const string SWORD_TAG = "Sword";

        [Inject]
        private void Construct(
            HealthCounter healthCounter,
            [InjectOptional(Id = SceneObjectId.Player)]GameObject playerOnScene,
            IFixedUpdateCallback fixedUpdate,
            CharacterController.CharacterController characterController)
        {
            _healthCounter = healthCounter;
            _playerOnScene = playerOnScene;
            fixedUpdate.OnFixedUpdate += OnFixedUpdate;
            _characterController = characterController;
        }

        public void SetStats(EnemyStats stats)
        {
            _speed = stats.Speed;
            _damage = stats.Damage;
            _health = stats.Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(SWORD_TAG))
            {
                if (_characterController.AttackedThisFrame)
                {
                    _health--;
                    if (_health <= 0)
                    {
                        OnDeath?.Invoke(this);
                    }

                    return;
                }
            }

            if (other.CompareTag(_playerOnScene.tag))
            {
                _healthCounter.TakeDamage(_damage);
                OnDeath?.Invoke(this);
            }
        }

        private void OnFixedUpdate()
        {
            if (_playerOnScene == null) return;
            transform.position = Vector3.MoveTowards(transform.position, _playerOnScene.transform.position, _speed * Time.deltaTime);
        }
    }
}