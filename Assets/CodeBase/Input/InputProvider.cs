using System.Collections.Generic;
using CodeBase.Input.Common;
using CodeBase.Input.Interfaces;

namespace CodeBase.Input
{
    public class InputProvider : IInputProvider
    {
        private List<IInputSource> _sources = new();
        private InputData _inputData = new InputData();
        
        public void AddSource(IInputSource source)
        {
            _sources.Add(source);
            _sources.Sort(((source1, source2) => source2.Priority.CompareTo(source1.Priority)));
        }

        public void RemoveSource(IInputSource source)
        {
            _sources.Remove(source);
        }

        public InputData GetInput()
        {
            foreach (IInputSource source in _sources)
            {
                source.FillInput(ref _inputData);
            }

            return _inputData;
        }
    }
}