using System.Collections.Generic;
using UnityEngine;

namespace Ignita.Utils.Extensions
{
    public static class ListExtension
    {
        public static T GetRandomElement<T>(this List<T> array)
        {
            int randomIndex = Random.Range(0, array.Count);
            return array[randomIndex];
        }
    }
}