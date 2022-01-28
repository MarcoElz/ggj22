using UnityEngine;

namespace Ignita.Utility
{
    public class FPSSimulator : MonoBehaviour
    {
        [SerializeField] int targetFrameRate = 60;

        private int normalVSyncCount;

        private void Start() => Debug.LogWarning("FPS Simulator must be on test builds only.");
        
        private void OnValidate() => Application.targetFrameRate = targetFrameRate;

        void OnEnable()
        {
            normalVSyncCount = QualitySettings.vSyncCount;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
        }


        private void OnDisable()
        {
            QualitySettings.vSyncCount = normalVSyncCount;
            Application.targetFrameRate = 60;
        }
    }
}
