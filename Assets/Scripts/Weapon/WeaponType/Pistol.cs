using System;
using CharacterStats.Interface;
using Cysharp.Threading.Tasks;
using Enemy.Pooling;
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
        [SerializeField] private ParticleSystem[] fireParticle;

        private WeaponConfig _weaponConfig;
        private IDamageStat _damageStat;
        private IObjectPool<ParticleSystem> _hitPool;
        private bool _isFired;

        [Inject]
        public void Construct(WeaponConfig config, IObjectPool<ParticleSystem> hitPool)
        {
            _weaponConfig = config;
            _hitPool = hitPool;
            
            _hitPool.Initialize(_weaponConfig.HitVFX);
        }

        public void Fire()
        {
            if(!_isFired) 
                return;

            foreach (var particle in fireParticle)
            {
                particle.Play();
            }
            
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

                var hitPoint = hit.point;
                var rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
                var impactEffect = _hitPool.Get(hitPoint, rotation);

                impactEffect.transform.rotation = rotation;
                impactEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                impactEffect.Clear(true);
                impactEffect.Play();

                var main = impactEffect.main;
                var totalLifetime = main.duration + main.startLifetime.constantMax;

                UniTask.Delay(TimeSpan.FromSeconds(totalLifetime),
                        cancellationToken: impactEffect.GetCancellationTokenOnDestroy())
                    .ContinueWith(() => _hitPool.Return(impactEffect))
                    .Forget();
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
