using UnityEngine;

namespace Weapon.Configs
{
    public interface IWeaponConfig
    {
        float SpeedFireInSecond { get; }
        LayerMask HitMask { get; }
        float MaxDistance { get; }
    }
}