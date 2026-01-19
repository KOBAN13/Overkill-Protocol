using Character.Interface;
using CharacterStats.Interface;
using Enemy.Interface;
using UnityEngine;
using UnityEngine.AI;
using IKill = Enemy.Interface.IKill;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class HumanoidEnemy : MonoBehaviour, IDamage, IInitializableEnemy
    {
        [SerializeField] private NavMeshAgent _agent;
        
        private IEnemyMove _enemyMove;
        private bool _isInitialized;
        public IDamageable Damagable { get; private set; }
        
        //TODO
        public IKill Kill { get; private set; }

        public void InitEnemy(IDamageable damageable, IEnemyMove enemyMove)
        {
            Damagable = damageable;
            _enemyMove = enemyMove;
            
            _enemyMove.InitMove(_agent);
        }
    }
}
