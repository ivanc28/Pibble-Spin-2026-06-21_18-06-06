using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private int currency;
    public HashSet<Upgrade> upgradesClaimed = new();

    public void IncreaseCurrency(int amt)
    {
        currency += amt;
        Debug.Log($"New Currency: {currency}");
    }
    public void SpendCurrency(int amt)
    {
        if(currency - amt >= 0)
        {
            currency -= amt;
        }
        else
        {
            Debug.LogWarning($"Only have {currency} currency but trying to spend {amt}");
        }
    }
    public int GetCurrency() 
    { 
        return currency;
    }
    public void AddUpgradeToClaimed(Upgrade upgrade)
    {
        upgradesClaimed.Add(upgrade);
    }

    public bool HasClaimedUpgrade(Upgrade upgrade)
    {
        return upgradesClaimed.Contains(upgrade);
    }

    public void ClearUpgrades()
    {
        upgradesClaimed.Clear();
    }

}
