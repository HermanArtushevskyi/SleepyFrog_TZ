using CodeBase.Common;
using CodeBase.GameFlow.ResultManager;
using UnityEngine;
using Zenject;
using CCharacterController = CodeBase.CharacterController.CharacterController;

namespace CodeBase.Factories
{
    public class PlayerFactory : Interfaces.IFactory<CCharacterController>
    {
        private readonly DiContainer _container;
        private readonly GameObject _prefab;
        private readonly Transform _spawnpoint;
        private readonly ResultManager _resultManager;

        public PlayerFactory(
            DiContainer container,
            [InjectOptional(Id = PrefabId.Player)] GameObject prefab,
            [InjectOptional(Id = SceneObjectId.Spawnpoint)] Transform spawnpoint,
            ResultManager resultManager)
        {
            _container = container;
            _prefab = prefab;
            _spawnpoint = spawnpoint;
            _resultManager = resultManager;
        }

        public CCharacterController Create()
        {
            GameObject playerOnScene =
                _container.InstantiatePrefab(_prefab, _spawnpoint.position, Quaternion.identity, null);
            _container.Bind<GameObject>().WithId(SceneObjectId.Player).FromInstance(playerOnScene).AsCached();
            CCharacterController characterController = playerOnScene.GetComponent<CCharacterController>();
            _container.Bind<CCharacterController>().FromInstance(characterController).AsCached();
            _resultManager.OnGameOver += () => GameObject.Destroy(playerOnScene);
            return characterController;
        }
    }
}