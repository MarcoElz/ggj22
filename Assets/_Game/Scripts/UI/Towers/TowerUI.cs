using System;
using _Game.InputHelper;
using _Game.Towers;
using _Game.UI.Utils;
using Ignita.Utils.Extensions;
using UnityEngine;

namespace _Game.UI.Towers
{
    public class TowerUI : FaderUI
    {
        [SerializeField] private float range = 5f;
        
        private Camera cam;
        private Camera Cam => cam == null ? cam = Camera.main : cam;

        private RectTransform rectTransform;
        
        private AbstractTower currentTower;
        private bool isActive;

        protected override void Awake()
        {
            base.Awake();
            rectTransform = GetComponent<RectTransform>();
            
            InstantHide();
        }
        
        //TODO: Move to other script, and refactor...
        private void Update()
        {
            var position = MouseHelper.Instance.WorldPoint;
            var tower = TowersManager.Instance.Elements.GetClosestElementInRange(position, range);

            if (isActive && tower == null)
            {
                Hide();
                isActive = false;
                currentTower = null;
            }
            else if (!isActive && tower != null)
            {
                Init(tower);
            }
            else if(isActive && !currentTower.Equals(tower))
            {
                Init(tower);
            }
        }

        private void LateUpdate()
        {
            if(!isActive) return;

            var position = Cam.WorldToScreenPoint(currentTower.transform.position);
            rectTransform.position = position;
        }

        public void Init(AbstractTower tower)
        {
            base.Show();
            currentTower = tower;
            isActive = true;
        }

    }
}