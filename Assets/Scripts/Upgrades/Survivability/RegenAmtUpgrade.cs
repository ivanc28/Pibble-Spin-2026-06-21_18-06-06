using UnityEngine;

[CreateAssetMenu(fileName = "Regen Amount Upgrade", menuName = "ScriptableData/Upgrades/Regeneration Amount")]
public class RegenAmtUpgrade : Upgrade
{
    [Tooltip("Proportion of regen amt mult increased")]
    public float regenAmtMult;
    public override void Apply(Player player)
    {
        player.stats.regenAmtMult += regenAmtMult;
    }
}

