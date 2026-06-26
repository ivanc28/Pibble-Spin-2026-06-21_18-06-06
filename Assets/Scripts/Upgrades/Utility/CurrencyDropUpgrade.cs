using UnityEngine;

[CreateAssetMenu(fileName = "Currency Drop Upgrade", menuName = "ScriptableData/Upgrades/Currency Drop")]
public class CurrencyDropUpgrade : Upgrade
{
    [Tooltip("Proportion of currency increased")]
    public float currencyDropMult;
    public override void Apply(Player player)
    {
        player.stats.currencyDropMult += currencyDropMult;
    }
}

