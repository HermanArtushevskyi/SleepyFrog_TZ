using System.Reflection;
using CodeBase.DataSaver.Common;
using CodeBase.DataSaver.Interfaces;
using UnityEngine;

namespace CodeBase.DataSaver
{
    public class PlayerPrefsSaver : ISaver, ILoader
    {
        public PlayerPrefsSaver()
        {
        }


        public bool Save(object data)
        {
            SavableAttribute savableAttribute = data.GetType().GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(savableAttribute.SaveName, json);
            return true;
        }

        public bool Load<T>(out T result)
        {
            result = default;
            SavableAttribute savableAttribute = typeof(T).GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;
            string json = PlayerPrefs.GetString(savableAttribute.SaveName);
            result = JsonUtility.FromJson<T>(json);
            return true;
        }
    }
}