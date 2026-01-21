using System;
using Character.Interface;
using CharacterStats.Interface;
using R3;
using UnityEngine;
using Weapon.Configs;
using Weapon.Interface;
using Zenject;

namespace Weapon.WeaponType
{
    public class Pistol : MonoBehaviour, IWeapon
    {
        [SerializeField] private Transform firePoint;

        private WeaponConfig _weaponConfig;
        private IDamageStat _damageStat;
        private bool _isFired;

        [Inject]
        public void Construct(WeaponConfig config)
        {
            _weaponConfig = config;
        }

        public void Fire()
        {
            if(!_isFired) 
                return;
            
            var origin =  firePoint.position;
            var direction = firePoint.forward;
            var maxDistance = _weaponConfig.MaxDistance;
            var hitMask = _weaponConfig.HitMask;

            Debug.DrawRay(origin, direction * maxDistance, Color.red, 1f);

            if (Physics.Raycast(origin, direction, out var hit, maxDistance, hitMask, QueryTriggerInteraction.Collide))
            {
                if (hit.collider.TryGetComponent(out IDamage damage))
                {
                    damage.Damagable.TakeDamage(_damageStat.CurrentDamage.CurrentValue);
                }
            }

            _isFired = false;
        }

        public void SetDamageStat(IDamageStat damageStat)
        {
            _damageStat = damageStat;
        }

        private void OnEnable()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(1 / _weaponConfig.SpeedFireInSecond), TimeSpan.FromSeconds(1 / _weaponConfig.SpeedFireInSecond))
                .Subscribe(_ => _isFired = true)
                .AddTo(this);
        }

        private void OnDisable()
        {
            _isFired = false;
        }
    }
}
