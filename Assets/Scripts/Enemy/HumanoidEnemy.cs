using System;
using Enemy.Interface;
using UnityEngine;
using UnityEngine.AI;
using IKill = Enemy.EnemyKill.IKill;

namespace Enemy
{
    public class HumanoidEnemy : MonoBehaviour, IHealth, IDamage, IInitializableEnemy
    {
        private IEnemyMove _enemyMove;
        public IHealthStats HealthStats { get; private set; }
        public IDamagable Damagable { get; private set; }
        public IKill Kill { get; private set; }
        public float PointsForDeath { get; private set; }

        public void InitEnemy(IHealthStats healthStats, IDamagable damagable, IEnemyMove enemyMove, IKill kill, float point)
        {
            if (healthStats == null || damagable == null || enemyMove == null)
                throw new ArgumentNullException();

            HealthStats = healthStats;
            Damagable = damagable;
            _enemyMove = enemyMove;
            PointsForDeath = point;
            Kill = kill;
            InitMove();
            Kill.OnTriggerEnemy(GetComponent<Collider>());
        }

        private void InitMove()
        {
            _enemyMove.InitMove(GetComponent<NavMeshAgent>());
        }
    }
}