using CodeBase.Input.Common;

namespace CodeBase.Input.Interfaces
{
    public interface IInputProvider
    {
        public void AddSource(IInputSource source);
        public void RemoveSource(IInputSource source);
        public InputData GetInput();
    }
}