using System.Collections.Generic;
using CodeBase.Factories.Interfaces;
using UnityEngine;

namespace CodeBase.ObjectPooling
{
    public class GameObjectPool : IPool<GameObject>
    {
        private Queue<GameObject> queue { get; } = new();
        
        public GameObjectPool(GameObject prefab, int initialSize, IFactory<GameObject, GameObject> gameObjectFactory)
        {
            for (int i = 0; i < initialSize; i++)
            {
                GameObject instance = gameObjectFactory.Create(prefab);
                instance.SetActive(false);
                queue.Enqueue(instance);
            }
        }

        public GameObject Get()
        {
            GameObject instance = queue.Dequeue();
            instance.SetActive(true);
            return instance;
        }

        public void Return(GameObject item)
        {
            item.SetActive(false);
            queue.Enqueue(item);
        }
    }
}