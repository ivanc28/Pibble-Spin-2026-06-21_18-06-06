using TMPro;
using UnityEngine;

public class UpgradeContainer : MonoBehaviour
{
    public Shrine shrine;
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDescription;
    private Upgrade upgrade;

    public void InitializeContainer(Upgrade randomUpgrade)
    {
        upgrade = randomUpgrade;
        upgradeName.text = upgrade.upgradeName;
        upgradeDescription.text = upgrade.description;
    }

    // Called by button
    public void ClaimUpgrade()
    {
        upgrade.Apply(Player.Instance);
        Player.Instance.inventory.AddUpgradeToClaimed(upgrade);
        foreach(Shrine shrine in FindObjectsByType<Shrine>(FindObjectsSortMode.None))
        {
            shrine.UpdateShrineCost(ShrineSpawner.Instance.GetShrineCost(Player.Instance.inventory.GetUpgradeCount()));
        }
        shrine.DisableShrine();
    }
}
