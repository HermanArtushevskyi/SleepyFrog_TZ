namespace CodeBase.Settings.Common
{
    public class SettingsBoolVariable : SettingsVariable<bool>
    {
        public SettingsBoolVariable(string name, bool value)
        {
            Name = name;
            Value = value;
        }

        public void Toggle() => Value = !Value;
        
        public void Set(bool value) => Value = value;
    }
}