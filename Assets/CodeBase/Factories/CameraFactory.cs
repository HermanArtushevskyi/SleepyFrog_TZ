using Cinemachine;
using CodeBase.Common;
using UnityEngine;
using Zenject;

namespace CodeBase.Factories
{
    public class CameraFactory : Interfaces.IFactory<Camera, GameObject>
    {
        private readonly DiContainer _container;
        private readonly GameObject _prefab;

        public CameraFactory(
            DiContainer container,
            [InjectOptional(Id = PrefabId.Camera)] GameObject prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public Camera Create(GameObject playerOnScene)
        {
            GameObject cameraOnScene =
                _container.InstantiatePrefab(_prefab, playerOnScene.transform.position, Quaternion.identity, null);
            _container.Bind<GameObject>().WithId(SceneObjectId.Camera).FromInstance(cameraOnScene).AsCached();
            Camera camera = cameraOnScene.GetComponent<Camera>();
            _container.Bind<Camera>().FromInstance(camera).AsSingle();
            CinemachineVirtualCamera cinemachineVirtualCamera = camera.GetComponentInChildren<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = playerOnScene.transform;
            cinemachineVirtualCamera.LookAt = playerOnScene.transform;
            return camera;
        }
    }
}