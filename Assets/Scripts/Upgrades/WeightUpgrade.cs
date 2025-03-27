using System;
using Events;

namespace Upgrades
{
    public class WeightUpgrade : Upgrade
    {
        protected override Action DoUpgrade()
        {
            return () => GameEventsManager.Instance.PlayerManager.MaxWeight += upgradeInfo.upgradeProgression;
        }

        protected override string SetInfoText()
        {
            return GameEventsManager.Instance.PlayerManager.MaxWeight.ToString();
        }

        protected override bool IsAtMax()
        {
            return GameEventsManager.Instance.PlayerManager.MaxWeight >= upgradeInfo.maxAmount;
        }
    }
}
