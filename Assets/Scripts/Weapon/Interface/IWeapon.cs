using CharactersStats.Interface;

namespace Weapon.Interface
{
    public interface IWeapon
    {
        void Fire();
        void SetDamageStat(IDamageStat damageStat);
    }
}