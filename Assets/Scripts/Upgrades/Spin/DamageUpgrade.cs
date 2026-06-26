using UnityEngine;

[CreateAssetMenu(fileName = "Damage Upgrade", menuName = "ScriptableData/Upgrades/Damage")]
public class DamageUpgrade : Upgrade
{
    [Tooltip("Proportion of base damaage increased")]
    public float damageMult;
    public override void Apply(Player player)
    {
        player.stats.damageMult += damageMult;
        foreach (Attack a in player.GetAvailableAttacks())
        {
            a.ApplyDamageModifier(player.stats.damageMult);
        }
    }
}
