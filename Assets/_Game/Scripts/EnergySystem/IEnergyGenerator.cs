namespace _Game.GameResources
{
    public interface IEnergyGenerator
    {
        bool IsGenerating { get; }
        float EnergyRate { get; }
    }
}