using System;
using System.Diagnostics;
using UnityEngine;

namespace _Game.InputHelper
{
    public class MouseHelper : MonoBehaviour
    {
        public static MouseHelper Instance { get; private set; }

        [SerializeField] private LayerMask layerMask = default;
        
        public Vector3 WorldPoint { get; private set; }
        
        private Camera cam;
        private Camera Cam => cam == null ? cam = Camera.main : cam;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            var ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                WorldPoint = hit.point;
            }
        }
    }
}