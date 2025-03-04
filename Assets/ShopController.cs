using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI speedCost;
    [SerializeField] private Button speedButton;
    [SerializeField] private TextMeshProUGUI weight;
    [SerializeField] private TextMeshProUGUI weightCost;
    [SerializeField] private Button weightButton;
    void Start()
    {
        InitSpeed();
        InitWeight();
    }

    private void InitSpeed()
    {
        if (CheckAfford(PlayerManager.inventory["Plastic"], PlayerManager.speedCost))
        {
            speedButton.interactable = true;
            speedCost.color = Color.green;
        }
        else
        {
            speedButton.interactable = false;
            speedCost.color = Color.red;
        }
        speed.text = PlayerManager.speed.ToString();
        speedCost.text = $"{PlayerManager.inventory["Plastic"]}/{PlayerManager.speedCost.ToString()} Plastic";
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
        InitSpeed();

    }

    public void UpgradeWeight()
    {
        if (PlayerManager.inventory["Metal"] < PlayerManager.weightCost) return;
        PlayerManager.MaxWeight += 10;
        PlayerManager.inventory["Metal"] -= PlayerManager.weightCost;
        PlayerManager.weightCost += 20;
        InitWeight();
    }

    private void InitWeight()
    {
        if (CheckAfford(PlayerManager.inventory["Metal"], PlayerManager.weightCost))
        {
            weightButton.interactable = true;
            weightCost.color = Color.green;
        }
        else
        {
            weightButton.interactable = false;
            weightCost.color = Color.red;
        }
        weight.text = PlayerManager.MaxWeight.ToString();
        weightCost.text = $"{PlayerManager.inventory["Metal"]}/{PlayerManager.weightCost.ToString()} Metal";
    }

    private bool CheckAfford(int curr, int cost)
    {
        return cost <= curr;
    }
}
