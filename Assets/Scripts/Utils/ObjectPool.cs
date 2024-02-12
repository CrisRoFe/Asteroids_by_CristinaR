using UnityEngine;
using System.Collections.Generic;

namespace Asteroids.Utils
{
    public static class ObjectPool
    {
        private static Dictionary<string, List<GameObject>> _pools;

        private const int DefaultPoolSize = 3;

        private static void InitNewPool(GameObject prefab)
        {
            //Create a pool of object with the default size and mark them
            //as inactive so they are ready to use
            var pooledObjects = new List<GameObject>();
            for (int i = 0; i < DefaultPoolSize; i++)
            {
                var obj = Object.Instantiate(prefab);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }

            _pools.Add(prefab.name, pooledObjects);
        }

        public static GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            _pools ??= new Dictionary<string, List<GameObject>>();

            if (!_pools.ContainsKey(prefab.name)) InitNewPool(prefab);
       
            //If there is a inactive object that is available to borrow then we
            //pick that one if all them are in use we create a new one and add it to the pool
            foreach (var item in _pools[prefab.name])
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.SetPositionAndRotation(pos, rot);
                    item.SetActive(true);
                    return item;
                }
            }

            var obj = Object.Instantiate(prefab, pos, rot);
            _pools[prefab.name].Add(obj);
            return obj;
        }

        public static void Despawn(GameObject go)
        {
            go.SetActive(false);
        }

        public static void CleanUpAllPools()
        {
            foreach (var pool in _pools)
            {
                foreach (var item in pool.Value)
                {
                    Despawn(item);
                    Object.Destroy(item);
                }
            }

            _pools.Clear();
        }
    }
}