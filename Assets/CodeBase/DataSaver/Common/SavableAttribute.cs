using System;

namespace CodeBase.DataSaver.Common
{
    public class SavableAttribute : Attribute
    {
        public readonly string SaveName;
        public readonly SaveMethod SaveMethod;
        
        public SavableAttribute(string saveName, SaveMethod saveMethod)
        {
            SaveName = saveName;
            SaveMethod = saveMethod;
        }
    }
}