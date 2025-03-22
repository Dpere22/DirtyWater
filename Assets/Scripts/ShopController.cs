using Events;
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
        if (CheckAfford(GameEventsManager.Instance.PlayerManager.Inventory["Plastic"], GameEventsManager.Instance.PlayerManager.SpeedCost))
        {
            speedButton.interactable = true;
            speedCost.color = Color.green;
        }
        else
        {
            speedButton.interactable = false;
            speedCost.color = Color.red;
        }
        speed.text = GameEventsManager.Instance.PlayerManager.SwimmingSpeed.ToString();
        speedCost.text = $"{GameEventsManager.Instance.PlayerManager.Inventory["Plastic"]}/{GameEventsManager.Instance.PlayerManager.SpeedCost.ToString()} Plastic";
    }

    /// <summary>
    /// Used by button
    /// </summary>
    public void UpgradeSpeed()
    {
        if (GameEventsManager.Instance.PlayerManager.Inventory["Plastic"] < GameEventsManager.Instance.PlayerManager.SpeedCost) return;
        GameEventsManager.Instance.PlayerManager.SwimmingSpeed += 1;
        GameEventsManager.Instance.PlayerManager.Inventory["Plastic"] -= GameEventsManager.Instance.PlayerManager.SpeedCost;
        GameEventsManager.Instance.PlayerManager.SpeedCost += 20;
        InitSpeed();

    }

    public void UpgradeWeight()
    {
        if (GameEventsManager.Instance.PlayerManager.Inventory["Metal"] < GameEventsManager.Instance.PlayerManager.WeightCost) return;
        GameEventsManager.Instance.PlayerManager.MaxWeight += 10;
        GameEventsManager.Instance.PlayerManager.Inventory["Metal"] -= GameEventsManager.Instance.PlayerManager.WeightCost;
        GameEventsManager.Instance.PlayerManager.WeightCost += 20;
        InitWeight();
    }

    private void InitWeight()
    {
        if (CheckAfford(GameEventsManager.Instance.PlayerManager.Inventory["Metal"], GameEventsManager.Instance.PlayerManager.WeightCost))
        {
            weightButton.interactable = true;
            weightCost.color = Color.green;
        }
        else
        {
            weightButton.interactable = false;
            weightCost.color = Color.red;
        }
        weight.text = GameEventsManager.Instance.PlayerManager.MaxWeight.ToString();
        weightCost.text = $"{GameEventsManager.Instance.PlayerManager.Inventory["Metal"]}/{GameEventsManager.Instance.PlayerManager.WeightCost.ToString()} Metal";
    }

    private bool CheckAfford(int curr, int cost)
    {
        return cost <= curr;
    }
}
