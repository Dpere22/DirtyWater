
public class UpgradeInfo
{
    public int PlasticCost;
    public int WoodCost;
    public int MetalCost;
    public bool Enabled;

    public UpgradeInfo(UpgradeInfoSO upgradeInfo)
    {
        PlasticCost = upgradeInfo.plasticCost;
        WoodCost = upgradeInfo.woodCost;
        MetalCost = upgradeInfo.metalCost;
        Enabled = upgradeInfo.enabled;
    }
    //
    // public void DoUpgrade()
    // {
    //     plasticCost += _upgradeInfo.plasticProgression;
    //     woodCost += _upgradeInfo.woodProgression;
    //     metalCost += _upgradeInfo.metalProgression;
    // }
}
