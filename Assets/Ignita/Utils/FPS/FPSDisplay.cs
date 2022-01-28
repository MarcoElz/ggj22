using UnityEngine;

namespace Ignita.Utility
{
    /// Original code by Aras Pranckevicius (NeARAZ)
    /// From: http://wiki.unity3d.com/index.php?title=FramesPerSecond
    /// I only add color field.

    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] Color labelColor = Color.white;

        float deltaTime = 0.0f;

        private void Start() => Debug.LogWarning("FPS Display must be on test builds only.");
        

        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = labelColor;
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}