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

        public GameObject Create(GameObject param)
        {
            GameObject gameobj = _container.InstantiatePrefab(param);
            gameobj.name = param.name;
            return gameobj;
        }
    }
}