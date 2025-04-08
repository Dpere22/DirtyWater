using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI plasticCountText;
        [SerializeField] private TextMeshProUGUI woodCountText;
        [SerializeField] private TextMeshProUGUI metalCountText;
        // Start is called once before the first execution of Update after the MonoBehaviour is created


        [SerializeField] private TextMeshProUGUI PlasticBottle;
        [SerializeField] private TextMeshProUGUI PlasticTrash;
        [SerializeField] private TextMeshProUGUI WoodenPlank;
        [SerializeField] private TextMeshProUGUI WoodenCrate;

        void OnEnable()
        {
            plasticCountText.text = GameEventsManager.Instance.PlayerManager.TotalPlastic.ToString();
            woodCountText.text = GameEventsManager.Instance.PlayerManager.TotalWood.ToString();
            metalCountText.text = GameEventsManager.Instance.PlayerManager.TotalMetal.ToString();

            PlasticBottle.text = "x" + GameEventsManager.Instance.PlayerManager.WaterBottle.ToString();
            PlasticTrash.text = "x" + GameEventsManager.Instance.PlayerManager.PlasticTrash.ToString();
            WoodenCrate.text = "x" + GameEventsManager.Instance.PlayerManager.WoodenCrate.ToString();
            WoodenPlank.text = "x" + GameEventsManager.Instance.PlayerManager.WoodenPlank.ToString();
        }

            }
}
