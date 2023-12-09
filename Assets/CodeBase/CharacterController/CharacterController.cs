using CodeBase.Input.Common;
using CodeBase.Input.Interfaces;
using UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.CharacterController
{
    public class CharacterController : MonoBehaviour
    {
        private IInputProvider _inputProvider;
        private IFixedUpdateCallback _fixedUpdate;
        private InputData _inputData;
        
        [Inject]
        private void Construct(IInputProvider inputProvider, IFixedUpdateCallback fixedUpdate)
        {
            _inputProvider = inputProvider;
            _fixedUpdate = fixedUpdate;
        }

        private void Start()
        {
            _fixedUpdate.OnFixedUpdate += OnFixedUpdate;
        }
        
        private void OnDestroy()
        {
            _fixedUpdate.OnFixedUpdate -= OnFixedUpdate;
        }

        private void OnFixedUpdate()
        {
            _inputData = _inputProvider.GetInput();
            Look();
            Hit();
        }

        private void Look()
        {
            if (_inputData.Horizontal != 0)
            {
                transform.rotation = Quaternion.Euler(0, _inputData.Horizontal > 0 ? 180 : 0, 0);
            }
        }

        private void Hit()
        {
        }
    }
}