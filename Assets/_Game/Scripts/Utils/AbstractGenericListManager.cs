using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Utils
{
    public abstract class AbstractGenericListManager<T> : MonoBehaviour where T : Component
    {
        private static AbstractGenericListManager<T> instance;

        public static AbstractGenericListManager<T> Instance => instance ?? FindObjectOfType<AbstractGenericListManager<T>>();

        public T[] Elements => array;
        
        private readonly List<T> list = new List<T>();
        protected T[] array;

        protected virtual void Awake()
        {
            if (instance == null)
                instance = this;
            
            RefreshArray();
        }
        
        public virtual void Add(T element)
        {
            list.Add(element);
            RefreshArray();
        }

        public virtual void Remove(T element)
        {
            list.Remove(element);
            RefreshArray();
        }

        private void RefreshArray() => array = list.ToArray();

        protected virtual bool IsValidElement(T element) => true;

        public T GetClosestValidElement(Vector3 position)
        {
            var closestValue = float.MaxValue;
            T closestElement = default(T);
            for (int i = 0; i < Elements.Length; i++)
            {
                var element = Elements[i];
                if (!IsValidElement(element)) continue;
                
                var sqrDistance = Vector3.SqrMagnitude(element.transform.position - position);
                if (sqrDistance < closestValue)
                {
                    closestElement = element;
                    closestValue = sqrDistance;
                }
            }

            return closestElement;
        }
    }
}