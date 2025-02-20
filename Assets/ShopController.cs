using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI cost;
    void Start()
    {
        speed.text = PlayerManager.speed.ToString();
        cost.text = $"{PlayerManager.inventory["Plastic"]}/{PlayerManager.speedCost.ToString()}";
    }

    /// <summary>
    /// Used by button
    /// </summary>
    public void UpgradeSpeed()
    {
        if (PlayerManager.inventory["Plastic"] < PlayerManager.speedCost) return;
        PlayerManager.speed += 1;
        PlayerManager.inventory["Plastic"] -= PlayerManager.speedCost;
        PlayerManager.speedCost += 20;
        speed.text = PlayerManager.speed.ToString();
        cost.text = $"{PlayerManager.inventory["Plastic"]}/{PlayerManager.speedCost.ToString()}";

    }
}
