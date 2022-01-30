using _Game.ShopSystem;
using _Game.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI.Builder
{
    public class BuildTowerButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameLabel = default;
        [SerializeField] private Image image = default;
        [SerializeField] private Button button = default;

        private TowerGeneralData currentTower;
        
        public void Init(TowerGeneralData tower)
        {
            currentTower = tower;
            nameLabel.text = tower.DisplayName;
            image.sprite = tower.Sprite;
            button.onClick.AddListener(OnPressed);
        }

        private void OnPressed()
        {
            var builder = FindObjectOfType<TowerBuilder>();
            builder.TryBuildMode(currentTower);
        }
    }
}