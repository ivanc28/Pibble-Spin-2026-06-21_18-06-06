using UnityEngine;

[CreateAssetMenu(fileName = "Health Upgrade", menuName = "ScriptableData/Upgrades/Health")]
public class HealthUpgrade : Upgrade
{
    [Tooltip("Proportion of max HP increased")]
    public float maxHealthMult;
    public override void Apply(Player player)
    {
        player.IncreaseMaxHPAndHPStat(maxHealthMult);
    }
}
