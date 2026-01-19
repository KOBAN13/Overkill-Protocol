using UnityEngine;

namespace Enemy.Interface
{
    public interface IKill
    {
        void OnTriggerEnemy(Collider collider);
    }
}