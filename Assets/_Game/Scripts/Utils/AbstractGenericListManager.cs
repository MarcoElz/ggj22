using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Utils
{
    public abstract class AbstractGenericListManager<T> : MonoBehaviour
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
    }
}