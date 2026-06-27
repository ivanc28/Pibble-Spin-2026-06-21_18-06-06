using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shrine : MonoBehaviour
{
    public float interactRange;
    public int numUpgradeOptions;
    public Canvas upgradeCanvas;
    public TextMeshPro costText;
    public UpgradeContainer[] upgradeContainers;
    public GameObject keyIcon;
    private UpgradePool upgradePool;
    private int currencyRequired;
    private bool inRange;
    public void Initialize(int currency)
    {
        UpdateShrineCost(currency);
        upgradePool = UpgradePool.Instance;
        upgradeCanvas.worldCamera = Camera.main;
        upgradeCanvas.sortingLayerName = "UI";
    }
    private void Update()
    {
        inRange = Player.Instance.StructureInRange(transform, interactRange);
        bool enoughCurrency = Player.Instance.inventory.GetCurrency() >= currencyRequired;
        costText.color = enoughCurrency ? Color.white : Color.red;
        if (inRange)
        {
            if(enoughCurrency)
            {
                keyIcon.SetActive(true);
            }
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (Player.Instance.inventory.SpendCurrency(currencyRequired))
                {
                    StartCoroutine(DisplayUpgrades());
                    costText.enabled = false;
                    keyIcon.SetActive(false);
                }
            }
        }
        else
        {
            keyIcon.SetActive(false);
        }
    }
    private IEnumerator DisplayUpgrades()
    {
        GameManager.Instance.SetGamePaused(true);
        EnableUpgradeCanvas(true);
        int numCategories = Enum.GetValues(typeof(Upgrade.UpgradeCategory)).Length;
        List<Upgrade.UpgradeCategory> categoryList = new();
        for(int i = 0; i < numCategories; i++)
        {
            categoryList.Add((Upgrade.UpgradeCategory)i);
        }
        yield return null;
        // Pick 3 random categories (all different) then select a random upgrade from that category
        for(int i = 0; i < numUpgradeOptions; i++)
        {
            Upgrade.UpgradeCategory randomCategory = categoryList[UnityEngine.Random.Range(0, categoryList.Count)];
            Upgrade[] upgradesFromRandomCategory = upgradePool.GetUpgradesFromCategory(randomCategory);
            while(upgradesFromRandomCategory.Length == 0)
            {
                randomCategory = categoryList[UnityEngine.Random.Range(0, categoryList.Count)];
                upgradesFromRandomCategory = upgradePool.GetUpgradesFromCategory(randomCategory);
            }
            Upgrade randomUpgrade = upgradePool.GetRandomUpgradeFromArray(upgradesFromRandomCategory, true);
            upgradeContainers[i].InitializeContainer(randomUpgrade);
            if (!randomUpgrade.repeatable)
            {
                upgradePool.RemoveUpgradeFromPool(randomCategory, randomUpgrade);
            }
            categoryList.Remove(randomCategory);
        }


    }
    private void EnableUpgradeCanvas(bool enabled)
    {
        upgradeCanvas.gameObject.SetActive(enabled);
    }
    public void UpdateShrineCost(int newCost)
    {
        currencyRequired = newCost;
        costText.text = $"Requires {newCost}";
    }

    public void DisableShrine()
    {
        EnableUpgradeCanvas(false);
        GameManager.Instance.SetGamePaused(false);
        ShrineSpawner.Instance.SpawnShrine();
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
