using CodeBase.Common;
using UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.EnemyBehaviour
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private GameObject _playerOnScene;
        private float _speed;
        private int _damage;
        private int _health;
        
        private const string SWORD_TAG = "Sword";
        
        public void SetStats(EnemyStats stats)
        {
            _speed = stats.Speed;
            _damage = stats.Damage;
            _health = stats.Health;
        }
        
        [Inject]
        private void Construct(GameObject playerOnScene, IFixedUpdateCallback fixedUpdate)
        {
            _playerOnScene = playerOnScene;
            fixedUpdate.OnFixedUpdate += OnFixedUpdate;
        }

        private void OnFixedUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerOnScene.transform.position, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerOnScene.tag))
            {
                Destroy(gameObject);
            }
            else if (other.CompareTag(SWORD_TAG))
            {
                _health--;
                if (_health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}