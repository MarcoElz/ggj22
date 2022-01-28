using Ignita.Utils.Extensions;

namespace _Game.Towers
{
    public class MusicTower : AbstractTower
    {
        private MusicData currentData;
        private MusicData CurrentData => currentData != null ? currentData : currentData = (MusicData) currentAbstractData;

        private void PlayClip()
        {
            var randomClip = CurrentData.MusicClips.GetRandomElement();
            //Play
        }
    }
}