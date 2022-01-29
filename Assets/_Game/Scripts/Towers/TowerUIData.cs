using System;
using UnityEngine;

namespace _Game.Towers
{
    [Serializable]
    public struct TowerUIData
    {
        [Header("General")] 
        public bool canBeDeleted;
        
        [Header("Special Action")] 
        public bool hasSpecialAction;
        public Sprite specialActionSprite;
    }
}