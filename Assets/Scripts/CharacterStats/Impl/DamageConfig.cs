using CharacterStats.Interface;
using CharacterStats.Stats;
using UnityEngine;

namespace CharacterStats.Impl
{
    [CreateAssetMenu(fileName = nameof(DamageConfig), menuName = "Stats" + "/" + nameof(DamageConfig))]
    public class DamageConfig : StatConfig, IDamageConfig
    {
        public override ECharacterStat StatType => ECharacterStat.Damage;

        [field: SerializeField]
        [field: Range(1, 1000)]
        public float BaseDamage { get; private set; }

        [field: SerializeField]
        [field: Range(5, 20)]
        public float BuffDamageInPercentage { get; private set; }

        [field: SerializeField]
        [field: Range(0, 200)]
        public float MaxBuffDamageInPercentage { get; private set; }
    }
}
