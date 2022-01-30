﻿using System;
using _Game.GameResources;
using _Game.InventorySystem;
using _Game.ShopSystem;
using _Game.Towers;
using UnityEngine;

namespace _Game
{
    public class Global : MonoBehaviour
    {
        public static Global Instance { get; private set; }

        [Header("Main References")] 
        [SerializeField] private Shop shop = default;
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private Dropper dropper = default;

        public static Shop Shop => Instance.shop;
        public static Inventory Inventory => Instance.inventory;
        public static Dropper Dropper => Instance.dropper;

        public static int Difficult => Instance.mainTower.UpgradeLevel + 1;
        
        //TODO Separate Resource into ResourceSettings
        [SerializeField] private Resource energyResource = default;
        [SerializeField] private Resource metalResource = default;
        [SerializeField] private Resource knowledgeResource = default;
        [SerializeField] private Resource contaminationResource = default;

        public static Resource Energy => Instance.energyResource;
        public static Resource Metal => Instance.metalResource;
        public static Resource Knowledge => Instance.knowledgeResource;
        public static Resource Contamination => Instance.contaminationResource;
        
        public static bool IsSpecialTowersUnlocked { get; private set; }

        private BaseTower mainTower;

        public static BaseTower MainTower
        {
            get
            {
                if (Instance.mainTower == null)
                    Instance.mainTower = FindObjectOfType<BaseTower>();

                return Instance.mainTower;
            }
        }
        
        private void Awake()
        {
            Instance = this;
        }

        public void UnlockSpecialTowers() => IsSpecialTowersUnlocked = true;
    }
}