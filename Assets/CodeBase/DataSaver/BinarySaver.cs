using System.IO;
using System.Reflection;
using CodeBase.DataSaver.Common;
using CodeBase.DataSaver.Interfaces;
using UnityEngine;

namespace CodeBase.DataSaver
{
    public class BinarySaver : ISaver, ILoader
    {
        private readonly string _filePath;

        public BinarySaver(string filePath)
        {
            _filePath = filePath;
        }

        public bool Save(object data)
        {
            SavableAttribute savableAttribute = data.GetType().GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;
            
            BinaryWriter binaryWriter = new BinaryWriter(File.Open(_filePath + savableAttribute.SaveName, FileMode.OpenOrCreate));

            binaryWriter.Write(data.GetType().FullName);
            binaryWriter.Write(JsonUtility.ToJson(data));
            binaryWriter.Close();
            binaryWriter.Dispose();
            return true;
        }

        public bool Load<T>(out T result)
        {
            result = default;
            SavableAttribute savableAttribute = typeof(T).GetCustomAttribute<SavableAttribute>();
            if (savableAttribute == null)
                return false;
            
            BinaryReader binaryReader = new BinaryReader(File.Open(_filePath + savableAttribute.SaveName, FileMode.OpenOrCreate));
            
            string typeName = binaryReader.ReadString();
            string json = binaryReader.ReadString();
            binaryReader.Close();

            if (typeName != typeof(T).FullName)
                return false;
            
            result = JsonUtility.FromJson<T>(json);
            binaryReader.Dispose();
            return true;
        }
    }
}