using UnityEngine;

namespace _Game.GameResources
{
    [CreateAssetMenu(fileName = "Resource_", menuName = "_Game/Resources/New Resource", order = 1)]
    public class Resource : ScriptableObject
    {
        [SerializeField] private Sprite icon;

        public Sprite Icon => icon;
    }
}