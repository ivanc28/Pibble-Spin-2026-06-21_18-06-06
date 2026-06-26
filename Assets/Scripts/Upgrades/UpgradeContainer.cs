using TMPro;
using UnityEngine;

public class UpgradeContainer : MonoBehaviour
{
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDescription;
    private Upgrade upgrade;

    public void InitializeContainer(Upgrade randomUpgrade)
    {
        upgrade = randomUpgrade;
        upgradeName.text = upgrade.upgradeName;
        upgradeDescription.text = upgrade.description;
    }
}
