using UnityEngine;
using UnityEngine.UI;

namespace Ignita.Utils.Extensions
{
    public static class UIExtension
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}