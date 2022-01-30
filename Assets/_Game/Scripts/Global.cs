using System;
using _Game.GameResources;
using _Game.ShopSystem;
using UnityEngine;

namespace _Game
{
    public class Global : MonoBehaviour
    {
        public static Global Instance { get; private set; }

        [Header("Main References")] [SerializeField]
        private Shop shop = default;

        public static Shop Shop => Instance.shop;
        
        //TODO Separate Resource into ResourceSettings
        [SerializeField] private Resource energyResource = default;
        [SerializeField] private Resource metalResource = default;
        [SerializeField] private Resource knowledgeResource = default;
        [SerializeField] private Resource contaminationResource = default;

        public static Resource Energy => Instance.energyResource;
        public static Resource Metal => Instance.metalResource;
        public static Resource Knowledge => Instance.knowledgeResource;
        public static Resource Contamination => Instance.contaminationResource;
        
        private void Awake()
        {
            Instance = this;
        }
    }
}