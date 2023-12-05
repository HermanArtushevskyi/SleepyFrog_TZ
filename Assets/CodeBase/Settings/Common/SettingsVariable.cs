using System;

namespace CodeBase.Settings.Common
{
    public abstract class SettingsVariable<T>
    {
        public string Name { get; protected set; }

        public T Value
        {
            get => _value;
            protected set
            {
                T previousValue = _value;
                _value = value;
                OnValueChanged?.Invoke(previousValue, _value);
                
            }
        }

        protected T _value;

        /// <summary>
        /// First T - previous value, second T - new value
        /// </summary>
        public event Action<T, T> OnValueChanged; 
    }
}