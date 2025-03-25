using Events;
using UnityEngine;

namespace Upgrades
{
    public class SpeedUpgrade : Upgrade
    {
        public override void DoUpgrade()
        {
            CheckRequirements();
            if (!CanUpgrade)
            {
                Debug.Log("Cannot Upgrade!");
                return;
            }
            Debug.Log("Upgrade Speed");
            GameEventsManager.Instance.PlayerManager.SwimmingSpeed += 1;
            ChargePlayer();
            SetUpgradeCosts();
            SetInfoText();
        }

        protected override void SetInfoText()
        {
            infoText.text = GameEventsManager.Instance.PlayerManager.SwimmingSpeed.ToString();
        }
    }
}
