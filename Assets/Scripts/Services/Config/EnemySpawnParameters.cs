using UnityEngine;

namespace Services.Config
{
    [CreateAssetMenu(fileName = nameof(EnemySpawnParameters), menuName = nameof(EnemySpawnParameters))]
    public class EnemySpawnParameters : ScriptableObject, IEnemySpawnParameters
    {
        [field: SerializeField] public float TimeToSpawnEnemy { get; private set; }
    }
}