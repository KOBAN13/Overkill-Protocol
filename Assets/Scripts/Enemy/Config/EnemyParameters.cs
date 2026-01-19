using UnityEngine;

namespace Enemy.Config
{
    [CreateAssetMenu(fileName = nameof(EnemyParameters), menuName = nameof(EnemyParameters))]
    public class EnemyParameters : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    }
}
