using System.Collections.Generic;
using UnityEngine;

namespace Ignita.Utils.ObjectPool
{
    public class ObjectPool
    {
    private readonly Queue<Component> objects;
    private readonly Component original;

    public ObjectPool(Component original)
    {
        objects = new Queue<Component>();
        this.original = original as Component;
    }

    public T Get<T>() where T : Component
    {
        if (objects.Count == 0)
            return Object.Instantiate(original as T);

        var obj = objects.Dequeue();
        obj.gameObject.SetActive(true);

        return obj as T;
    }
    
    public T Get<T>(Transform parent) where T : Component
    {
        if (objects.Count == 0)
            return Object.Instantiate(original as T, parent);

        var obj = objects.Dequeue();
        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(true);

        return obj as T;
    }
    
    

    public void Pool(Component obj) => objects.Enqueue(obj);
    }
}