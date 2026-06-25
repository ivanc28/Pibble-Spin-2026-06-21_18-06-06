using UnityEngine;

[CreateAssetMenu(fileName = "Move Speed Upgrade", menuName = "ScriptableData/Upgrades/Move Speed")]
public class MoveSpeedUpgrade : Upgrade
{
    [Tooltip("Proportion of base move speed increased")]
    public float moveSpeedMult;
    public override void Apply(Player player)
    {
        player.stats.moveSpeedMult += moveSpeedMult;
    }
}
