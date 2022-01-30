using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_Base_0", menuName = "_Game/Towers/Specifics/Base", order = 1)]
    public class BaseTowerData : AbstractSpecificTowerData
    {
        [SerializeField] private TowerGeneralData[] unlockedTowers = default;
        [SerializeField] private TowerGeneralData[] specialTowers = default;

        public TowerGeneralData[] UnlockedTowers => unlockedTowers;
        public TowerGeneralData[] SpecialTowers => specialTowers;
    }
}