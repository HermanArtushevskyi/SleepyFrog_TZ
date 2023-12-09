using UnityEngine;

namespace CodeBase.EnemyBehaviour
{
    [CreateAssetMenu(fileName = "ScriptableEnemy", menuName = "ScriptableObjs/ScriptableEnemy", order = 0)]
    public class ScriptableEnemy : ScriptableObject
    {
        [SerializeField] public GameObject Prefab;
        [SerializeField] public EnemyStats Stats;
    }
}