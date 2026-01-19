using UnityEngine;

namespace Enemy.Pooling
{
    public interface IEnemyPool
    {
        HumanoidEnemy Get(Vector3 position, Quaternion rotation);
        void Release(HumanoidEnemy enemy);
    }
}
