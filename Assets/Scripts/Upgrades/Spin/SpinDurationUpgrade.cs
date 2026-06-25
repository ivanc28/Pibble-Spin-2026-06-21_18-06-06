using UnityEngine;

[CreateAssetMenu(fileName = "Spin Duration Upgrade", menuName = "ScriptableData/Upgrades/Spin Duration")]
public class SpinDurationUpgrade : Upgrade
{
    public float additionalSpinTime;
    public override void Apply(Player player)
    {
        player.stats.spinLifetimeIncrease += additionalSpinTime;
    }
}
