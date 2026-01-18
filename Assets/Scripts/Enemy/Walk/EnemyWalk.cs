using System;
using R3;
using UnityEngine.AI;
using Zenject;

namespace Enemy.Walk
{
    public class EnemyWalk : IEnemyMove, IDisposable, ITickable
    {
        private readonly ITarget _target;
        private readonly float _speed;
        private NavMeshAgent _navMeshAgent;
        private readonly CompositeDisposable _compositeDisposable = new();
        
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

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}