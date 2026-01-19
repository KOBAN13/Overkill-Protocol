using CharacterStats.Health.Interface;
using Enemy.Interface;
using UnityEngine;
using UnityEngine.AI;
using IKill = Enemy.Interface.IKill;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class HumanoidEnemy : MonoBehaviour, IHealth, IDamage, IInitializableEnemy
    {
        [SerializeField] private NavMeshAgent _agent;
        
        private IEnemyMove _enemyMove;
        public IHealthStats HealthStats { get; private set; }
        public IDamagable Damagable { get; private set; }
        public IKill Kill { get; private set; }

        public void InitEnemy(IHealthStats healthStats, IDamagable damagable, IEnemyMove enemyMove, IKill kill)
        {
            HealthStats = healthStats;
            Damagable = damagable;
            _enemyMove = enemyMove;
            Kill = kill;
            
            _enemyMove.InitMove(_agent);
            Kill.OnTriggerEnemy(GetComponent<Collider>());
        }
    }
}