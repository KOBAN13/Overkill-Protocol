using Character;
using Enemy.Health.Die;
using R3;
using R3.Triggers;
using UnityEngine;

namespace Enemy.EnemyKill
{
    public class HumanoidEnemyKill : IKill
    {
        private Die<PlayerComponents> _player;
        
        private CompositeDisposable _compositeDisposable = new();

        public HumanoidEnemyKill(Die<PlayerComponents> player)
        {
            _player = player;
        }

        public void OnTriggerEnemy(Collider collider)
        {
            PlayerComponents playerComponents = null;
            
            collider
                .OnTriggerEnterAsObservable()
                .Subscribe(_ => _player.Died(playerComponents))
                .AddTo(_compositeDisposable);
        }
    }

    public interface IKill
    {
        void OnTriggerEnemy(Collider collider);
    }
}