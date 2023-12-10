using System.Collections.Generic;
using CodeBase.Factories.Interfaces;
using UnityEngine;

namespace CodeBase.ObjectPooling
{
    public class AudioSourcePool : IPool<AudioSource>
    {
        private GameObject _prefab;
        private IFactory<GameObject, GameObject> _gameObjectFactory;
        
        private Queue<AudioSource> queue { get; } = new();
        
        public AudioSourcePool(GameObject prefab, int initialSize, IFactory<GameObject, GameObject> gameObjectFactory)
        {
            _prefab = prefab;
            _gameObjectFactory = gameObjectFactory;
            
            for (int i = 0; i < initialSize; i++)
            {
                CreateInstance(prefab, gameObjectFactory);
            }
        }

        public AudioSource Get()
        {
            if (queue.Count == 0)
                CreateInstance(_prefab, _gameObjectFactory);
            AudioSource instance = queue.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Return(AudioSource item)
        {
            item.gameObject.SetActive(false);
            queue.Enqueue(item);
        }

        private void CreateInstance(GameObject prefab, IFactory<GameObject, GameObject> gameObjectFactory)
        {
            GameObject instance = gameObjectFactory.Create(prefab);
            instance.SetActive(false);
            queue.Enqueue(instance.GetComponent<AudioSource>());
        }
    }
}