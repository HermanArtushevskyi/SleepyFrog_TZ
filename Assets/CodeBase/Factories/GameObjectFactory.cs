using UnityEngine;
using Zenject;

namespace CodeBase.Factories
{
    public class GameObjectFactory : Interfaces.IFactory<GameObject, GameObject>
    {
        private readonly DiContainer _container;

        public GameObjectFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject param) => _container.InstantiatePrefab(param);
    }
}