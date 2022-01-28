using Ignita.Utils.Extensions;
using UnityEngine;

namespace _Game.Towers
{
    public class MusicTower : AbstractTower
    {
        [SerializeField] private AudioSource audioSource = default;
        
        private MusicData currentData;
        private MusicData CurrentData => currentData != null ? currentData : currentData = (MusicData) currentAbstractData;

        public override void TurnOn()
        {
            base.TurnOn();
            Play();
        }

        public override void TurnOff()
        {
            base.TurnOff();
            Stop();
        }

        private void Play()
        {
            var randomClip = CurrentData.MusicClips.GetRandomElement();
            audioSource.clip = randomClip;
            //TODO: Maybe add some SFX of a vinyl player
            audioSource.Play(1);
        }

        private void Stop()
        {
            audioSource.Stop();
            //TODO: Maybe add some SFX of an stopping player
        }
    }
}