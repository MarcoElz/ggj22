using UnityEngine;

namespace Ignita.Utils.Extensions
{
    public static class TransformExtension
    {
        #region LocalPosition

        public static void SetLocalPositionX(this Transform transform, float value)
        {
            var position = transform.localPosition;
            position.x = value;
            transform.localPosition = position;
        }
        
        public static void SetLocalPositionY(this Transform transform, float value)
        {
            var position = transform.localPosition;
            position.y = value;
            transform.localPosition = position;
        }
        
        public static void SetLocalPositionZ(this Transform transform, float value)
        {
            var position = transform.localPosition;
            position.z = value;
            transform.localPosition = position;
        }

        #endregion
        
        #region WorldPosition

        public static void SetPositionX(this Transform transform, float value)
        {
            var position = transform.position;
            position.x = value;
            transform.localPosition = position;
        }
        
        public static void SetPositionY(this Transform transform, float value)
        {
            var position = transform.position;
            position.y = value;
            transform.localPosition = position;
        }
        
        public static void SetPositionZ(this Transform transform, float value)
        {
            var position = transform.position;
            position.z = value;
            transform.localPosition = position;
        }

        #endregion
        
    }
}