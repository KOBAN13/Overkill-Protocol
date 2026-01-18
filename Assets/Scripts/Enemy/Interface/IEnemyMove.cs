using UnityEngine.AI;

namespace Enemy.Interface
{
    public interface IEnemyMove
    {
        void InitMove(NavMeshAgent agent);
    }
}