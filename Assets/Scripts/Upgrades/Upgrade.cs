using UnityEngine;

public abstract class Upgrade : ScriptableObject
{
    public string upgradeName;
    [TextArea(2, 6)]
    public string description;
    [TextArea(2, 2)]
    public string flavorText;
    public enum UpgradeCategory { Movement, Spin, Health };
    public UpgradeCategory upgradeCategory;
    public int rarityWeight;
    [Tooltip("Whether this upgrade can be applied again")]
    public bool repeatable;
    [Tooltip("Optional prerequisite upgrade that must be claimed before this upgrade can be available")]
    public Upgrade prerequisiteUpgrade;
    public Sprite sprite;
    /// <summary>
    /// Applies this upgrade to the player
    /// </summary>
    /// <param name="player"></param>
    public abstract void Apply(Player player);
    /// <summary>
    /// Determines whether this upgrade should be available to the player
    /// </summary>
    /// <returns>True if this upgrade should be considered in the Upgrade Pool, false otherwise</returns>
    public virtual bool IsAvailable()
    {
        bool meetsUpgradePrequisite = prerequisiteUpgrade == null || Player.Instance.inventory.HasClaimedUpgrade(prerequisiteUpgrade);
        return meetsUpgradePrequisite;
    }

}
