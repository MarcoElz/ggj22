using System;
using MEC;
using UnityEngine;

namespace Ignita.Utils.ObjectPool
{
    public static class PoolMecExtension
    {
        // public static void ReturnToPoolDelayed(this PoolManager poolManager, float delay, GameObject obj) =>
        //     Timing.CallDelayed(delay, () => PoolManager.ReturnToPool(obj));
        
        public static void ReturnToPoolDelayed(this PoolManager poolManager, float delay, IPoolObject obj) =>
            Timing.CallDelayed(delay, () => PoolManager.ReturnToPool(obj));
        
        public static void ReturnToPoolDelayed(this IPoolObject obj, float delay) =>
            Timing.CallDelayed(delay, () => PoolManager.ReturnToPool(obj));
        
    }
}