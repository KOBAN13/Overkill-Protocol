using Character.Interface;
using CharactersStats.Interface;
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
        public IDamageable Damageable { get; private set; }
        
        //TODO
        public IKill Kill { get; private set; }

        public void InitEnemy(IDamageable damageable, IEnemyMove enemyMove)
        {
            Damageable = damageable;
            _enemyMove = enemyMove;
            
            _enemyMove.InitMove(_agent);
        }
    }
}
