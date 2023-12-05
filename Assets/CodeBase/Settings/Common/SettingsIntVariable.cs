namespace CodeBase.Settings.Common
{
    public class SettingsIntVariable : SettingsVariable<int>
    {
        public SettingsIntVariable(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public void Increment() => Value++;
        public void Decrement() => Value--;
        public void Set(int value) => Value = value;
    }
}