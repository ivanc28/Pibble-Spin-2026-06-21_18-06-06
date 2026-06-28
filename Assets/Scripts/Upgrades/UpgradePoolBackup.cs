// using System.Collections.Generic;
// using UnityEngine;

// public class UpgradePool : MonoBehaviour
// {
//     public UpgradeCategory[] poolOfUpgrades;
//     // Singleton Instantiation
//     public static UpgradePool Instance { get; private set; }
//     private void Awake()
//     {
//         if(Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//         DontDestroyOnLoad(gameObject);
//     }

//     /// <summary>
//     /// Returns a random upgrade from the given array using the specified method (weighted or unweighted)
//     /// </summary>
//     /// <param name="upgradeArr"></param>
//     /// <param name="weighted"></param>
//     /// <returns></returns>
//     public Upgrade GetRandomUpgradeFromArray(Upgrade[] upgradeArr, bool weighted)
//     {
//         Upgrade upgrade = null;
//         if (!weighted)
//         {
//             int randomUpgradeIndex = Random.Range(0, upgradeArr.Length);
//             upgrade = upgradeArr[randomUpgradeIndex];
//         }
//         else
//         {
//             int totalWeight = 0;
//             foreach (Upgrade currUpgrade in upgradeArr)
//             {
//                 totalWeight += currUpgrade.rarityWeight;
//             }
//             int roll = Random.Range(1, totalWeight + 1);
//             int accumulator = 0;
//             foreach (Upgrade currUpgrade in upgradeArr)
//             {
//                 accumulator += currUpgrade.rarityWeight;
//                 if (roll <= accumulator)
//                 {
//                     upgrade = currUpgrade;
//                     break;
//                 }
//             }
//         }

//         if (upgrade != null)
//         {
//             return upgrade;
//         }
//         else
//         {
//             Debug.LogError("Failed to retrieve random upgrade");
//             return null;
//         }

//     }

//     /// <summary>
//     /// Returns an array of available upgrades that are all in the specified category
//     /// </summary>
//     /// <param name="category"></param>
//     /// <returns></returns>
//     public Upgrade[] GetUpgradesFromCategory(Upgrade.UpgradeCategory category)
//     {
//         List<Upgrade> upgradeList = new();
//         foreach (Upgrade upgrade in poolOfUpgrades[(int)category].upgrades)
//         {
//             if (upgrade.IsAvailable())
//             {
//                 upgradeList.Add(upgrade);
//             }
//         }
//         return upgradeList.ToArray();
//     }
//     /// <summary>
//     /// Removes the specified upgrade from the specified upgrade category (Removes from upgrade pool)
//     /// </summary>
//     /// <param name="category"></param>
//     /// <param name="upgrade"></param>
//     public void RemoveUpgradeFromPool(Upgrade.UpgradeCategory category, Upgrade upgrade)
//     {
//         poolOfUpgrades[(int)category].upgrades.Remove(upgrade);
//     }
// }
