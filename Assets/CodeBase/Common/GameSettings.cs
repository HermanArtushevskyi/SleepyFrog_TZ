using UnityEngine;

namespace CodeBase.Common
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjs/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] public int HealthAmount;
        [SerializeField] public AnimationCurve EnemySpawnCurve;
        [SerializeField] public float MaxComboDelay;
    }
}