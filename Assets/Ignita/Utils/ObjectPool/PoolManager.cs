using System.Collections.Generic;
using CommonUtils;
using CommonUtils.ComponentCaching;
using CommonUtils.Extensions;
using UnityEngine;

namespace Ignita.Utils.ObjectPool
{
    public class PoolManager : MonoBehaviour, IVerbosable
    {
        [SerializeField] private bool verbose = false;
        
        public static PoolManager Instance { get; set; }
        
        private Dictionary<string, ObjectPool> pools;

        public bool IsVerbose => verbose;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            pools = new Dictionary<string, ObjectPool>();
        }

        public static T Spawn<T>(T original) where T : Component
        {
            string id = original.name;
            VerifyPool(original, id);

            var obj = Instance.pools[id].Get<T>();
            InitializeObject(obj, id);
            
            return obj;
        }
        
        public static T Spawn<T>(T original, Transform parent) where T : Component
        {
            string id = original.name;
            VerifyPool(original, id);

            var obj = Instance.pools[id].Get<T>(parent);
            InitializeObject(obj, id);
            
            return obj;
        }

        private static void VerifyPool(Component original, string id)
        {
            if (!Instance.pools.ContainsKey(id)) 
                Instance.pools.Add(id, new ObjectPool(original));
        }

        private static void InitializeObject<T> (T obj, string id)
        {
            var poolObject = ((IPoolObject) obj);
            poolObject.PoolId = id;
            poolObject.OnSpawnFromPool();
        }

        public static T Spawn<T>(T original, Vector3 position) where T : Component
        {
            var obj = Spawn<T>(original);
            obj.transform.position = position;
            return obj;
        }
        
        public static T Spawn<T>(T original, Vector3 position, Quaternion rotation) where T : Component
        {
            var obj = Spawn<T>(original);
            obj.transform.SetPositionAndRotation(position, rotation);
            return obj;
        }

        public static void ReturnToPool(IPoolObject poolObject)
        {
            if (string.IsNullOrEmpty(poolObject.PoolId) || !Instance.pools.ContainsKey(poolObject.PoolId))
            {
                Instance.DebugLog($"Object {poolObject.name} has no pool. Destroying instead...");
                Destroy(poolObject.gameObject);
                return;
            }
            
            poolObject.OnReturnToPool();
            poolObject.transform.SetParent(null);
            poolObject.gameObject.SetActive(false);
            Instance.pools[poolObject.PoolId].Pool(poolObject as Component);
        }
        public static void ReturnToPool(GameObject clone)
        {
            var poolObject = clone.GetCachedComponent<IPoolObject>();

            if (poolObject == null)
            {
                Instance.DebugLog($"Object {clone.name} it's not an IPoolObject. Destroying instead...");
                Destroy(clone);
                return;
            }
            
            ReturnToPool(poolObject);

        }

        /*public void PoolObject(GameObject original, GameObject clone, float delay)
        {
            StartCoroutine(PoolObjectCoroutine(original, clone, delay));
        }

        private IEnumerator PoolObjectCoroutine(GameObject original, GameObject clone, float delay)
        {
            yield return new WaitForSeconds(delay);
            PoolObject(original, clone);
        }*/
        
    }
}