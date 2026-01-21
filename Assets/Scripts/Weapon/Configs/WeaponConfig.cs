using UnityEngine;

namespace Weapon.Configs
{
    [CreateAssetMenu(fileName = nameof(WeaponConfig), menuName = nameof(WeaponConfig) + "/" + "Pistol")]
    public class WeaponConfig : ScriptableObject, IWeaponConfig
    {
        [field: SerializeField] public float SpeedFireInSecond { get; private set; }
        [field: SerializeField] public LayerMask HitMask { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public ParticleSystem HitVFX { get; private set; }
    }
}