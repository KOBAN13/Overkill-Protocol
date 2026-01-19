using UnityEngine;

namespace Enemy.Factory
{
    public interface IEnemyFactory
    {
        HumanoidEnemy Spawn(Vector3 position, Quaternion rotation);
        void Despawn(HumanoidEnemy enemy);
    }
}
