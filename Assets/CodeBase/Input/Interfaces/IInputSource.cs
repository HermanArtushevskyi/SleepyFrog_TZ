using CodeBase.Input.Common;

namespace CodeBase.Input.Interfaces
{
    public interface IInputSource
    {
        public int Priority { get; }
        
        public void FillInput(ref InputData inputData);
    }
}