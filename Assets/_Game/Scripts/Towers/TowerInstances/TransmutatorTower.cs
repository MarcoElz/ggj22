using UnityEngine;

namespace _Game.Towers
{
    public class TransmutatorTower : AbstractTower
    {
        
        private TransmutatorData currentData;
        private TransmutatorData CurrentData => currentData != null ? currentData : currentData = (TransmutatorData) CurrentAbstractData;
        protected override void Update()
        {
            if(!IsOn) return;
            base.Update();
            
            Transmutate();
            
        }

        private void Transmutate()
        {
            float thisFrame = CurrentData.Rate * Time.deltaTime;
            Global.Inventory.Add(CurrentData.Resource, thisFrame);
        }
    }
}