using UnityEngine;

namespace _Game.Towers
{
    [CreateAssetMenu(fileName = "T_Music_0", menuName = "_Game/Towers/Specifics/Music", order = 1)]
    public class MusicData : AbstractSpecificTowerData
    {
        [SerializeField] private AudioClip[] musicClips = default;

        public AudioClip[] MusicClips => musicClips;
    }
}