using System;
using UnityEngine;

namespace _Game
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private float zoomSpeed = 1f;
        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }


        private void Update()
        {
            var amountToZoom = -Input.mouseScrollDelta.y;
            amountToZoom = amountToZoom * zoomSpeed * Time.deltaTime;
            cam.orthographicSize += amountToZoom;

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1f, 12f);
        }
    }
}
