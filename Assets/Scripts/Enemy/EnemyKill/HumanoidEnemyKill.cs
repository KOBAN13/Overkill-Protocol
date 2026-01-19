using System;
using Character;
using CharacterStats.Health.Die;
using Enemy.Interface;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Enemy.EnemyKill
{
    public class HumanoidEnemyKill : IKill, IDisposable
    {
        private readonly IDie<PlayerComponents> _playerDie;
        private readonly PlayerComponents _playerComponents;
        private readonly CompositeDisposable _compositeDisposable = new();
        
        public HumanoidEnemyKill(IDie<PlayerComponents> playerDie, PlayerComponents playerComponents)
        {
            _playerDie = playerDie;
            _playerComponents = playerComponents;
        }

        public void OnTriggerEnemy(Collider collider)
        {
            collider
                .OnTriggerEnterAsObservable()
                .Subscribe(_ => _playerDie.Died(_playerComponents))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}
