using System;
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
        
        private void OnEnable()
        {
            _upgradeManager = FindFirstObjectByType<UpgradeManager>(); //semi bad code
            _info = _upgradeManager.GetUpgradeById(upgradeInfo.upgradeId);
            ResetVisual();
        }

        private void Update()
        {
            ResetVisual();
        }

        private bool CheckRequirements()
        {
            int currPlastic = GameEventsManager.Instance.PlayerManager.TotalPlastic;
            int currWood = GameEventsManager.Instance.PlayerManager.TotalWood;
            int currMetal = GameEventsManager.Instance.PlayerManager.TotalMetal;
            if (currPlastic < _info.plasticCost || currWood < _info.woodCost || currMetal < _info.metalCost)
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
            int currMetal = GameEventsManager.Instance.PlayerManager.TotalMetal;
            int currWood = GameEventsManager.Instance.PlayerManager.TotalWood;
            if (IsAtMax())
            {
                plasticCostText.text = "";
                metalCostText.text = "";
                woodCostText.text = "";
            }
            else
            {
                plasticCostText.text = _info.plasticCost != 0 ? $"{currPlastic} / {_info.plasticCost}" : "";
                metalCostText.text = _info.metalCost != 0 ? $"{currMetal} / {_info.metalCost}" : "";
                woodCostText.text = _info.woodCost != 0 ? $"{currWood} / {_info.woodCost}" : "";
                plasticCostText.color = currPlastic < _info.plasticCost ? Color.red : Color.green;
                metalCostText.color = currMetal < _info.metalCost ? Color.red : Color.green;
                woodCostText.color = currWood < _info.woodCost ? Color.red : Color.green;
            }
        }

        private void ChargePlayer()
        {
            GameEventsManager.Instance.PlayerManager.TotalPlastic -= _info.plasticCost;
            GameEventsManager.Instance.PlayerManager.TotalWood -= _info.woodCost;
            GameEventsManager.Instance.PlayerManager.TotalMetal -= _info.metalCost;
        }
    }
}
