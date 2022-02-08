using System.Collections.Generic;
using UnityEngine;

namespace Ignita.Utils.Extensions
{
    public static class ArrayExtension
    {
        public static T GetRandomElement<T>(this T[] array)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }

        public static T GetClosestElement<T>(this T[] elements, Vector3 position) where T : Component
        {
            var closestValue = float.MaxValue;
            T closestElement = null;
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i];
                var sqrDistance = Vector3.SqrMagnitude(element.transform.position - position);
                if (sqrDistance < closestValue)
                {
                    closestElement = element;
                    closestValue = sqrDistance;
                }
            }

            return closestElement;
        }
        
        public static T GetClosestElementInRange<T>(this T[] elements, Vector3 position, float range) where T : Component
        {
            var sqrRange = range * range;
            var closestValue = float.MaxValue;
            T closestElement = null;
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i];
                var sqrDistance = Vector3.SqrMagnitude(element.transform.position - position);
                if(sqrDistance > sqrRange) continue;

                if (sqrDistance < closestValue)
                {
                    closestElement = element;
                    closestValue = sqrDistance;
                }
            }

            return closestElement;
        }
        
        //TODO: Maybe add an out T[] and return the count to reduce GC Alloc 
        public static List<T> GetAllElementInRange<T>(this T[] elements, Vector3 position, float range) where T : Component
        {
            var sqrRange = range * range;

            var elementsInRange = new List<T>();
            
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i];
                var sqrDistance = Vector3.SqrMagnitude(element.transform.position - position);
                if(sqrDistance > sqrRange) continue;

                elementsInRange.Add(element);
            }

            return elementsInRange;
        }
        
        public static List<T> GetAllElementInRange<T>(this T[] elements, Vector3 position, float range, List<T> list) where T : Component
        {
            var sqrRange = range * range;
            
            list.Clear();
            for (int i = 0; i < elements.Length; i++)
            {
                var element = elements[i];
                var sqrDistance = Vector3.SqrMagnitude(element.transform.position - position);
                if(sqrDistance > sqrRange) continue;

                list.Add(element);
            }

            return list;
        }
    }
    
    
}