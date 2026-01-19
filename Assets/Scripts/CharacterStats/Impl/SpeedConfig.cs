using CharacterStats.Interface;
using UnityEngine;

namespace CharacterStats.Impl
{
    [CreateAssetMenu(fileName = nameof(SpeedConfig), menuName = "Stats" + "/" + nameof(SpeedConfig))]
    public class SpeedConfig : ScriptableObject, IStaminaConfig
    {
        [field: SerializeField] public float BaseSpeed { get; private set; }
        [field: SerializeField] public float BuffSpeedInPercentage { get; private set; }
    }
}