using System.Reflection;
using CodeBase.DataSaver.Common;
using CodeBase.DataSaver.Interfaces;

namespace CodeBase.DataSaver
{
    public class UniversalSaver : ISaver, ILoader
    {
        private readonly BinarySaver _binarySaver;
        private readonly JsonSaver _jsonSaver;
        private readonly PlayerPrefsSaver _playerPrefsSaver;
        
        public UniversalSaver(SaveSettings saveSettings)
        {
            _binarySaver = new BinarySaver(saveSettings.PersistentDataPath);
            _jsonSaver = new JsonSaver(saveSettings.PersistentDataPath);
            _playerPrefsSaver = new PlayerPrefsSaver();
        }

        public bool Save(object data)
        {
            SavableAttribute savableAttribute = data.GetType().GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;

            switch (savableAttribute.SaveMethod)
            {
                case SaveMethod.Binary:
                    return _binarySaver.Save(data);
                case SaveMethod.Json:
                    return _jsonSaver.Save(data);
                case SaveMethod.PlayerPrefs:
                    return _playerPrefsSaver.Save(data);
                default:
                    return false;
            }
        }

        public bool Load<T>(out T result)
        {
            result = default;
            SavableAttribute savableAttribute = typeof(T).GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;

            switch (savableAttribute.SaveMethod)
            {
                case SaveMethod.Binary:
                    return _binarySaver.Load(out result);
                case SaveMethod.Json:
                    return _jsonSaver.Load(out result);
                case SaveMethod.PlayerPrefs:
                    return _playerPrefsSaver.Load(out result);
                default:
                    return false;
            }
        }
    }
}