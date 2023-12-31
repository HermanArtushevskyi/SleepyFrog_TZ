﻿using System.Collections.Generic;
using CodeBase.Factories.Interfaces;
using UnityEngine;

namespace CodeBase.ObjectPooling
{
    public class GameObjectPool : IPool<GameObject>
    {
        private GameObject _prefab;
        private IFactory<GameObject, GameObject> _gameObjectFactory;
        
        private Queue<GameObject> queue { get; } = new();
        
        public GameObjectPool(GameObject prefab, int initialSize, IFactory<GameObject, GameObject> gameObjectFactory)
        {
            _prefab = prefab;
            _gameObjectFactory = gameObjectFactory;
            
            for (int i = 0; i < initialSize; i++)
            {
                CreateInstance(prefab, gameObjectFactory);
            }
        }

        public GameObject Get()
        {
            if (queue.Count == 0)
                CreateInstance(_prefab, _gameObjectFactory);
            GameObject instance = queue.Dequeue();
            instance.SetActive(true);
            return instance;
        }

        public void Return(GameObject item)
        {
            item.SetActive(false);
            queue.Enqueue(item);
        }

        private void CreateInstance(GameObject prefab, IFactory<GameObject, GameObject> gameObjectFactory)
        {
            GameObject instance = gameObjectFactory.Create(prefab);
            instance.SetActive(false);
            queue.Enqueue(instance);
        }
    }
}