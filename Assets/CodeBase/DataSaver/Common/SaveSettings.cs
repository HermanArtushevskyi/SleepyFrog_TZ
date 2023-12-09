using UnityEngine;

namespace CodeBase.DataSaver.Common
{
    [UnityEngine.CreateAssetMenu(fileName = "SaveSettings", menuName = "ScriptableObjs/SaveSettings", order = 0)]
    public class SaveSettings : ScriptableObject
    {
        [SerializeField] public SaveMethod SaveMethod;
    }
}