using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrades
{
    public abstract class Upgrade : MonoBehaviour
    {
        [SerializeField] protected Button button;
        [SerializeField] protected TextMeshProUGUI plasticCostText;
        [SerializeField] protected TextMeshProUGUI woodCostText;
        [SerializeField] protected TextMeshProUGUI metalCostText;
        [SerializeField] protected UpgradeInfoSO upgradeInfo;
        [SerializeField] protected TextMeshProUGUI infoText;
        private UpgradeInfo _info;
        private UpgradeManager _upgradeManager;
        protected bool CanUpgrade;

        private void Start()
        {
            _upgradeManager = FindFirstObjectByType<UpgradeManager>(); //semi bad code
            _info = _upgradeManager.GetUpgradeById(upgradeInfo.upgradeId);
            CheckRequirements();
            SetUpgradeCosts();
            SetInfoText();
        }

        protected void CheckRequirements()
        {
            int currPlastic = GameEventsManager.Instance.PlayerManager.Inventory["Plastic"];
            int currWood = GameEventsManager.Instance.PlayerManager.Inventory["Wood"];
            int currMetal = GameEventsManager.Instance.PlayerManager.Inventory["Metal"];
            if (currPlastic < _info.plasticCost || currWood < _info.woodCost || currMetal < _info.metalCost)
            {
                CanUpgrade = false;
            }
            else
            {
                CanUpgrade = true;
            }
        }
        public abstract void DoUpgrade();
        protected abstract void SetInfoText();

        protected void SetUpgradeCosts()
        {
            int currPlastic = GameEventsManager.Instance.PlayerManager.Inventory["Plastic"];
            int currMetal = GameEventsManager.Instance.PlayerManager.Inventory["Metal"];
            int currWood = GameEventsManager.Instance.PlayerManager.Inventory["Wood"];
            plasticCostText.text =
                $"{currPlastic} / {_info.plasticCost}";
            metalCostText.text =
                $"{currMetal} / {_info.metalCost}";
            woodCostText.text =
                $"{currWood} / {_info.woodCost}";
            plasticCostText.color = currPlastic < _info.plasticCost ? Color.red : Color.green;
            metalCostText.color = currMetal < _info.metalCost ? Color.red : Color.green;
            woodCostText.color = currWood < _info.woodCost ? Color.red : Color.green;
        }

        protected void ChargePlayer()
        {
            GameEventsManager.Instance.PlayerManager.Inventory["Plastic"] -= _info.plasticCost;
            GameEventsManager.Instance.PlayerManager.Inventory["Wood"] -= _info.woodCost;
            GameEventsManager.Instance.PlayerManager.Inventory["Metal"] -= _info.metalCost;
        }
    }
}
