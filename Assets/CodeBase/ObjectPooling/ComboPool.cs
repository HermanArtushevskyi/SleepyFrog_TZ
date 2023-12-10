using System.Collections.Generic;
using CodeBase.Factories.Interfaces;
using CodeBase.GameFlow.KillCounter;
using UnityEngine;

namespace CodeBase.ObjectPooling
{
    public class ComboPool : IPool<ComboBehaviour>
    {
        private readonly GameObject _prefab;
        private readonly IFactory<GameObject, GameObject> _gameObjectFactory;
        
        private Queue<ComboBehaviour> queue { get; } = new();

        public ComboPool(GameObject prefab, int initialSize, IFactory<GameObject, GameObject> gameObjectFactory)
        {
            _prefab = prefab;
            _gameObjectFactory = gameObjectFactory;
            
            for (int i = 0; i < initialSize; i++)
            {
                CreateInstance(prefab, gameObjectFactory);
            }
        }

        public ComboBehaviour Get()
        {
            if (queue.Count == 0)
                CreateInstance(_prefab, _gameObjectFactory);
            ComboBehaviour instance = queue.Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Return(ComboBehaviour item)
        {
            item.gameObject.SetActive(false);
            queue.Enqueue(item);
        }

        private void CreateInstance(GameObject prefab, IFactory<GameObject, GameObject> gameObjectFactory)
        {
            GameObject instance = gameObjectFactory.Create(prefab);
            instance.SetActive(false);
            queue.Enqueue(instance.GetComponent<ComboBehaviour>());
        }
    }
}