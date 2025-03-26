using System.Collections.Generic;
using UnityEngine;

namespace Upgrades
{
    public class UpgradeManager : MonoBehaviour
    {
        private readonly Dictionary<string, UpgradeInfo> _upgrades = new();

        private void Start()
        {
            LoadUpgrades();
        }

        private void LoadUpgrades()
        {
            UpgradeInfoSO[] allUpgrades = UnityEngine.Resources.LoadAll<UpgradeInfoSO>("Upgrades");
            foreach (UpgradeInfoSO upgrade in allUpgrades)
            {
                _upgrades[upgrade.upgradeId] = new UpgradeInfo(upgrade);
            }
        }

        public UpgradeInfo GetUpgradeById(string upgradeId)
        {
            return _upgrades[upgradeId];
        }
    }
}
