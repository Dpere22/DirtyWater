using UnityEngine;

public class UpgradeInfo
{
    public int plasticCost;
    public int woodCost;
    public int metalCost;
    private readonly UpgradeInfoSO _upgradeInfo;

    public UpgradeInfo(UpgradeInfoSO upgradeInfo)
    {
        _upgradeInfo = upgradeInfo;
        plasticCost = upgradeInfo.plasticCost;
        woodCost = upgradeInfo.woodCost;
        metalCost = upgradeInfo.metalCost;
    }

    public void DoUpgrade()
    {
        plasticCost += _upgradeInfo.plasticProgression;
        woodCost += _upgradeInfo.woodProgression;
        metalCost += _upgradeInfo.metalProgression;
    }
}
