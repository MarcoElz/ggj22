using System;
using _Game.GameResources;
using _Game.InventorySystem;
using _Game.ShopSystem;
using _Game.Towers;
using _Game.UI.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game
{
    //TODO: Improve this mess
    //Sorry future self for making this. I was in a hurry :c
    //At least is not that much. 
    public class Global : MonoBehaviour
    {
        public static Global Instance { get; private set; }

        [Header("Main References")] 
        [SerializeField] private Shop shop = default;
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private Dropper dropper = default;
        [SerializeField] private FaderUI gameOverScreen = default;

        public static Shop Shop => Instance.shop;
        public static Inventory Inventory => Instance.inventory;
        public static Dropper Dropper => Instance.dropper;

        public static int Difficult => Instance.isGameOver ? 1 :  Instance.mainTower.UpgradeLevel + 1; //TODO: Clean this
        
        //TODO: Separate Resource into ResourceSettings
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
            IsSpecialTowersUnlocked = false;
        }

        public void UnlockSpecialTowers() => IsSpecialTowersUnlocked = true;

        public void Exit() => Application.Quit();
        
        //TODO: Make a better Pause System
        private bool isPaused;

        public void TogglePause()
        {
            if(isPaused)
                UnPause();
            else
                Pause();
        }
        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f;
        }

        public void UnPause()
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
        
        
        //TODO: Improve
        private bool isGameOver;
        public static bool IsGameOver => Instance.isGameOver;

        public void GameOver()
        {
            isGameOver = true;
            gameOverScreen.gameObject.SetActive(true);
            gameOverScreen.Show();
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}