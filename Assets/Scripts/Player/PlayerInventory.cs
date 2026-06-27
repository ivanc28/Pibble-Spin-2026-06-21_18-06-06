using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private int currency;
    public HashSet<Upgrade> upgradesClaimed = new();
    private int numUpgradesClaimed;

    public void IncreaseCurrency(int amt)
    {
        currency += amt;
        Debug.Log($"New Currency: {currency}");
    }
    public bool SpendCurrency(int amt)
    {
        if(currency - amt >= 0)
        {
            currency -= amt;
            return true;
        }
        else
        {
            Debug.LogWarning($"Only have {currency} currency but trying to spend {amt}");
            return false;
        }
    }
    public int GetCurrency() 
    { 
        return currency;
    }
    public void AddUpgradeToClaimed(Upgrade upgrade)
    {
        upgradesClaimed.Add(upgrade);
        numUpgradesClaimed++;
    }

    public bool HasClaimedUpgrade(Upgrade upgrade)
    {
        return upgradesClaimed.Contains(upgrade);
    }
    public int GetUpgradeCount()
    {
        return numUpgradesClaimed;
    }
    public void ClearUpgrades()
    {
        upgradesClaimed.Clear();
    }

}
