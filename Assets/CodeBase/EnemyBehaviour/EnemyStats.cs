using UnityEngine;

namespace CodeBase.EnemyBehaviour
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjs/EnemyStats", order = 0)]
    public class EnemyStats : ScriptableObject
    {
        public float Speed;
        public int Health;
        public int Damage;
    }
}