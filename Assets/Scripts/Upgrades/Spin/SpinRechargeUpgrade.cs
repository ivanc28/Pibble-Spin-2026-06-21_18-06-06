using UnityEngine;

[CreateAssetMenu(fileName = "Spin Recharge Upgrade", menuName = "ScriptableData/Upgrades/Spin Recharge")]
public class SpinRechargeUpgrade : Upgrade
{
    [Tooltip("Additional amount of charge/sec")]
    public float additionalChargeRate;
    public override void Apply(Player player)
    {
        player.stats.spinRechargeRateIncrease += additionalChargeRate;
    }
}
