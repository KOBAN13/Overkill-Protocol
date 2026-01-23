using CharactersStats.Interface;
using CharactersStats.Stats;
using UnityEngine;

namespace CharactersStats.Impl
{
    [CreateAssetMenu(fileName = nameof(SpeedConfig), menuName = "Stats" + "/" + nameof(SpeedConfig))]
    public class SpeedConfig : StatConfig, ISpeedConfig
    {
        public override ECharacterStat StatType => ECharacterStat.Speed;

        [field: SerializeField]
        [field: Range(10, 1000)] public float BaseSpeed { get; private set; }
        
        [field: SerializeField]
        [field: Range(5, 20)] public float BuffSpeedInPercentage { get; private set; }
        
        [field: SerializeField]
        [field: Range(0, 200)] public float MaxBuffSpeedInPercentage { get; private set; }
    }
}
