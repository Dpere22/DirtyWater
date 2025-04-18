using System;
using Events;
using Upgrades;

public class CrateUpgradeUI : Upgrade
{
    protected override Action DoUpgrade()
    {
        return () =>
        {
            GameEventsManager.Instance.PlayerManager.MaxDropOffCrates++;
            GameEventsManager.Instance.PlayerManager.DropOffCrates = GameEventsManager.Instance.PlayerManager.MaxDropOffCrates;
        };
    }

    protected override string SetInfoText()
    {
        return GameEventsManager.Instance.PlayerManager.MaxDropOffCrates.ToString();
    }

    protected override bool IsAtMax()
    {
        return GameEventsManager.Instance.PlayerManager.MaxDropOffCrates >= upgradeInfo.maxAmount;
    }
}
