namespace _Game.Towers
{
    public class GuardianTower : AbstractTower
    {
        private GuardianData currentData;
        private GuardianData CurrentData => (GuardianData) CurrentAbstractData;
    }
}