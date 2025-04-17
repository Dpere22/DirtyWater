using System;
using Events;
using TMPro;
using UnityEngine;

namespace Upgrades
{
    public abstract class Upgrade : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI plasticCostText;
        [SerializeField] protected TextMeshProUGUI woodCostText;
        [SerializeField] protected UpgradeInfoSO upgradeInfo;
        [SerializeField] protected TextMeshProUGUI infoText;
        [SerializeField] protected TextMeshProUGUI descriptionText;
        private UpgradeInfo _info;
        private UpgradeManager _upgradeManager;
        
        private void OnEnable()
        {
            _upgradeManager = FindFirstObjectByType<UpgradeManager>(); //semi bad code
            _info = _upgradeManager.GetUpgradeById(upgradeInfo.upgradeId);
            if (!CheckAvailable()) return;
            if(descriptionText) descriptionText.text = upgradeInfo.description;
            ResetVisual();
        }

        private bool CheckAvailable()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(_info.Enabled);
            }
            return _info.Enabled;

        }

        private void Update()
        {
            ResetVisual();
        }

        private bool CheckRequirements()
        {
            int currPlastic = GameEventsManager.Instance.PlayerManager.TotalPlastic;
            int currWood = GameEventsManager.Instance.PlayerManager.TotalWood;
            if (currPlastic < _info.PlasticCost || currWood < _info.WoodCost)
            {
                return false;
            }
            return true;
        }
        public void OnUpgradeButtonPressed()
        {
            if (!CheckRequirements() || IsAtMax()) return;
            var upgradeFunc = DoUpgrade();
            upgradeFunc();
            ChargePlayer();
        }
        protected abstract Action DoUpgrade();
        protected abstract string SetInfoText();

        protected abstract bool IsAtMax();

        private void SetInfoTextInternal(string text)
        {
            infoText.text = text;
        }

        private void ResetVisual()
        {
            SetInfoTextInternal(IsAtMax() ? "MAX" : SetInfoText());
            SetUpgradeCosts();
        }

        private void SetUpgradeCosts()
        {
            int currPlastic = GameEventsManager.Instance.PlayerManager.TotalPlastic;
            int currWood = GameEventsManager.Instance.PlayerManager.TotalWood;
            if (IsAtMax())
            {
                plasticCostText.text = "";
                woodCostText.text = "";
            }
            else
            {
                plasticCostText.text = _info.PlasticCost != 0 ? $"{currPlastic} / {_info.PlasticCost}" : "";
                woodCostText.text = _info.WoodCost != 0 ? $"{currWood} / {_info.WoodCost}" : "";
                plasticCostText.color = currPlastic < _info.PlasticCost ? Color.red : Color.green;
                woodCostText.color = currWood < _info.WoodCost ? Color.red : Color.green;
            }
        }

        private void ChargePlayer()
        {
            GameEventsManager.Instance.PlayerManager.TotalPlastic -= _info.PlasticCost;
            GameEventsManager.Instance.PlayerManager.TotalWood -= _info.WoodCost;
        }
    }
}
