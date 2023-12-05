using CodeBase.Input.Common;
using CodeBase.Input.Interfaces;

namespace CodeBase.Input
{
    public class InputClearer : IInputSource
    {
        public int Priority => 0;
        
        public void FillInput(ref InputData inputData)
        {
            inputData.Horizontal = 0f;
            inputData.Spacebar = false;
        }
    }
}