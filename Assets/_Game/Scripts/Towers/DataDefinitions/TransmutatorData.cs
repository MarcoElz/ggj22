using _Game.GameResources;
using _Game.Towers;
using UnityEngine;

[CreateAssetMenu(fileName = "T_Transmutator", menuName = "_Game/Towers/Specifics/Transmutator", order = 1)]
public class TransmutatorData : AbstractSpecificTowerData
{
    [SerializeField] private Resource resource = default;
    [SerializeField] private float rate = 1f;

    public Resource Resource => resource;
    public float Rate => rate;
}