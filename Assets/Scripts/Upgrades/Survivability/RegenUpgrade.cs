using UnityEngine;

[CreateAssetMenu(fileName = "Regen Rate Upgrade", menuName = "ScriptableData/Upgrades/Regeneration Rate")]
public class RegenUpgrade: Upgrade
{
    [Tooltip("Proportion of regen mult increased")]
    public float regenMult;
    public override void Apply(Player player)
    {
        player.stats.regenRateMult += regenMult;
    }
}

