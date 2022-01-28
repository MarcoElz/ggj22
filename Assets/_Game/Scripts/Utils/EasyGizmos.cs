using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Game.Utils
{
    public static class EasyGizmos
    {
        public static void DrawWireDisc(Vector3 position, Vector3 normal, float radius, Color color)
        {
            #if UNITY_EDITOR
            Handles.color = color;
            Handles.DrawWireDisc(position, normal, radius);
            #endif
        }
    }
}