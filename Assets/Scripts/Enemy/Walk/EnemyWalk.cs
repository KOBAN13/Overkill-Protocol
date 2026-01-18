using Character.Interface;
using Enemy.Interface;
using UnityEngine.AI;
using Zenject;

namespace Enemy.Walk
{
    public class EnemyWalk : IEnemyMove, ITickable
    {
        private readonly ITarget _target;
        private readonly float _speed;
        private NavMeshAgent _navMeshAgent;
        
        public EnemyWalk(ITarget target, float speed)
        {
            _target = target;
            _speed = speed;
        }
        
        public void Tick()
        {
            _navMeshAgent.SetDestination(_target.GetTarget());
        }

        public void InitMove(NavMeshAgent agent)
        {
            _navMeshAgent = agent;
            _navMeshAgent.speed = _speed;
        }
    }
}