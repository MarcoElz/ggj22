using System;

namespace _Game.GameResources
{
    
    [Serializable]
    public struct ResourceAmount
    {
        public AbstractResourceData resource;
        public float amount;
    }
}