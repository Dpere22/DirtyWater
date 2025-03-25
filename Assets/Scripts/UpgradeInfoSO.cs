using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeInfoSO", menuName = "Scriptable Objects/UpgradeInfoSO")]
public class UpgradeInfoSO : ScriptableObject
{
    [SerializeField] public string upgradeId;
    [SerializeField] public int plasticCost;
    [SerializeField] public int woodCost;
    [SerializeField] public int metalCost;
    [SerializeField] public int plasticProgression;
    [SerializeField] public int woodProgression;
    [SerializeField] public int metalProgression;
    [SerializeField] public int maxAmount;
    [SerializeField] public int upgradeProgression;
}
