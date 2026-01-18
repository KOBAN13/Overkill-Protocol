using System;
using Character.Interface;
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
            
            var origin = firePoint != null ? firePoint.position : transform.position;
            var direction = firePoint != null ? firePoint.forward : transform.forward;
            var maxDistance = _weaponConfig.MaxDistance;
            var hitMask = _weaponConfig.HitMask;

            if (Physics.Raycast(origin, direction, out var hit, maxDistance, hitMask, QueryTriggerInteraction.Ignore))
            {
                var damageable = hit.collider.GetComponentInParent<IDamageable>();
                
                if (damageable != null)
                {
                    damageable.TakeDamage(_weaponConfig.Damage);
                }
            }

            _isFired = false;
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
