using System;
using Events;

namespace Upgrades
{
    public class OxygenUpgrade : Upgrade
    {
        protected override Action DoUpgrade()
        {
            return () =>
            {
                GameEventsManager.Instance.PlayerManager.MaxTime += upgradeInfo.upgradeProgression;
            };
        }

        protected override string SetInfoText()
        {
            return $"{GameEventsManager.Instance.PlayerManager.MaxTime}";
        }

        protected override bool IsAtMax()
        {
            return GameEventsManager.Instance.PlayerManager.MaxTime >= upgradeInfo.maxAmount;
        }
    }
}
