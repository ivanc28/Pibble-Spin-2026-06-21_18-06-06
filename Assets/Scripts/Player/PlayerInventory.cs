using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private int currency;
    //private HashSet<Item> items = new(); // hashset used later to store upgrade pickups

    public void IncreaseCurrency(int amt)
    {
        currency += amt;
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
}
