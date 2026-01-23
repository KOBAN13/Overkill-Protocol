using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Enemy.Pooling
{
    public class CustomObjectPool<T> : IObjectPool<T> where T : Component
    {
        private const int DEFAULT_CAPACITY = 8;
        private const int MAX_CAPACITY = 64;
        
        private T _prefab;
        private ObjectPool<T> _pool;
        private readonly DiContainer _diContainer;

        public CustomObjectPool(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize(T effect)
        {
            _prefab = effect;
            
            _pool = new ObjectPool<T>(
                Create, 
                OnGet, 
                OnRelease, 
                OnDestroy, 
                true, 
                DEFAULT_CAPACITY, 
                MAX_CAPACITY
            );
        }
        
        private static void OnDestroy(T obj)
        {
            Object.Destroy(obj.gameObject);
        }
        
        private static void OnRelease(T obj)
        {
            if (obj != null)
                return;
            
            obj.gameObject.SetActive(false);
        }
        
        private static void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }
            
        private T Create()
        {
            var enemy = _diContainer.InstantiatePrefabForComponent<T>(_prefab);
            
            return enemy;
        }
        
        public T Get(Vector3 position, Quaternion rotation)
        {
            var poolObject = _pool.Get();
            poolObject.transform.SetPositionAndRotation(position, rotation);
            return poolObject;
        }

        public void Return(T obj)
        {
            _pool.Release(obj);
        }

        public void Release(T obj)
        {
            _pool.Release(obj);
        }
    }
}