using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrades;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> upgrades;
    private void OnEnable()
    {
        //This code is sort of a band-aid fix - goal is to enable and disable upgrades depending on if they are in the Upgrade Manager
        Dictionary<string, GameObject> upgradesDictionary = upgrades.ToDictionary(upgrade => upgrade.GetComponent<Upgrade>().id);
        UpgradeManager upgradeManager = FindFirstObjectByType<UpgradeManager>();
        foreach (var upgrade in upgradesDictionary.Keys)
        {
            upgradesDictionary[upgrade].gameObject.SetActive(upgradeManager.GetUpgradeById(upgrade).Enabled);
        }
    }
}
