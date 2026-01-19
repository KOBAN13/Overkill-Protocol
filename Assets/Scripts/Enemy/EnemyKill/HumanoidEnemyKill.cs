using System;
using CharacterStats.Health.Die;
using Enemy.Interface;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Enemy.EnemyKill
{
    public class HumanoidEnemyKill : IKill, IDisposable
    {
        private readonly IDie _playerDie;
        private readonly CompositeDisposable _compositeDisposable = new();
        
        public HumanoidEnemyKill(IDie playerDie)
        {
            _playerDie = playerDie;
        }

        public void OnTriggerEnemy(Collider collider)
        {
            collider
                .OnTriggerEnterAsObservable()
                .Subscribe(_ => _playerDie.Died())
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}
