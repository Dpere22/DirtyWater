using System;
using Events;
using UnityEngine;

namespace Upgrades
{
    public class SpeedUpgrade : Upgrade
    {
        protected override Action DoUpgrade()
        {
            return () => GameEventsManager.Instance.PlayerManager.SwimmingSpeed += upgradeInfo.upgradeProgression;
        }

        protected override string SetInfoText()
        {
            return GameEventsManager.Instance.PlayerManager.SwimmingSpeed.ToString();
        }

        protected override bool IsAtMax()
        {
            return GameEventsManager.Instance.PlayerManager.SwimmingSpeed >= upgradeInfo.maxAmount;
        }
    }
}
