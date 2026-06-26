using UnityEngine;

[CreateAssetMenu(fileName = "Pickup Range Upgrade", menuName = "ScriptableData/Upgrades/Pickup Range")]
public class PickupRangeUpgrade : Upgrade
{
    [Tooltip("Proportion of pickup range mult increased")]
    public float pickupRangeMult;
    public override void Apply(Player player)
    {
        player.stats.pickupRangeMult += pickupRangeMult;
    }
}

