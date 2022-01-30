using System;
using _Game.GameResources;

namespace _Game.ShopSystem
{
    [Serializable]
    public struct Transaction
    {
        public ResourceAmount[] resourceCosts;
    }
}