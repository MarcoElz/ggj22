using UnityEngine;

namespace Ignita.Utils.Extensions
{
    public static class RectTransformExtension
    {
        #region AnchoredPosition

        public static void SetAnchoredPositionX(this RectTransform transform, float value)
        {
            var position = transform.anchoredPosition;
            position.x = value;
            transform.anchoredPosition = position;
        }
        
        public static void SetAnchoredPositionY(this RectTransform transform, float value)
        {
            var position = transform.anchoredPosition;
            position.y = value;
            transform.anchoredPosition = position;
        }

        #endregion
        
        
    }
}