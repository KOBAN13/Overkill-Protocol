using UnityEngine;

namespace Enemy.Config
{
    [CreateAssetMenu(fileName = nameof(EnemyParameters), menuName = nameof(EnemyParameters))]
    public class EnemyParameters : ScriptableObject
    {
        [field: Header("Health Randomization")]
        [field: SerializeField]
        [field: Range(1, 10)]
        public int MinStartHealth { get; private set; } = 1;

        [field: SerializeField]
        [field: Range(1, 10)]
        public int MaxStartHealth { get; private set; } = 10;

        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    }
}
