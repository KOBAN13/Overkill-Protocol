using UnityEngine;

namespace Enemy.Pooling
{
    public interface IObjectPool<T>  where T : Component
    {
        void Initialize(T effect);
        T Get(Vector3 position, Quaternion rotation);
        void Release(T obj);
        void Return(T obj);
    }
}
