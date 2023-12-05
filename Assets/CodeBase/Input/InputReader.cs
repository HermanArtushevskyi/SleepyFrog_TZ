using CodeBase.Input.Common;
using CodeBase.Input.Interfaces;
using UnityEngine;
using UnityInput;

namespace CodeBase.Input
{
    public class InputReader : IInputSource
    {
        public int Priority => 1;
        
        private readonly InputActions _inputActions;

        public InputReader(InputActions inputActions)
        {
            inputActions.Enable();
            _inputActions = inputActions;
        }

        public void FillInput(ref InputData inputData)
        {
            inputData.Horizontal = _inputActions.Player.Direction.ReadValue<Vector2>().x;
            inputData.Spacebar = _inputActions.Player.Spacebar.triggered;
        }
    }
}