using CharacterStats.Interface;
using CharacterStats.Stats;
using UnityEngine;

namespace CharacterStats.Impl
{
    [CreateAssetMenu(fileName = nameof(HealthConfig), menuName = "Stats" + "/" + nameof(HealthConfig))]
    public class HealthConfig : StatConfig, IHealthConfig
    {
        public override ECharacterStat StatType => ECharacterStat.Health;

        [field: SerializeField]
        [field: Range(10, 1000)]
        public float BaseValue { get; private set; }

        [field: SerializeField]
        [field: Range(5, 20)]
        public float BuffHealthInPercentage { get; private set; }
        
        [field: SerializeField]
        [field: Range(0, 200)]
        public float MaxBuffHealthInPercentage { get; private set; }
    }
}
