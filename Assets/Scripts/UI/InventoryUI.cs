using Events;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI plasticCountText;
        [SerializeField] private TextMeshProUGUI woodCountText;
        [SerializeField] private TextMeshProUGUI metalCountText;


        [FormerlySerializedAs("PlasticBottle")] [SerializeField] private TextMeshProUGUI plasticBottle;
        [FormerlySerializedAs("PlasticTrash")] [SerializeField] private TextMeshProUGUI plasticTrash;
        [FormerlySerializedAs("WoodenPlank")] [SerializeField] private TextMeshProUGUI woodenPlank;
        [FormerlySerializedAs("WoodenCrate")] [SerializeField] private TextMeshProUGUI woodenCrate;

        void OnEnable()
        {
            plasticCountText.text = GameEventsManager.Instance.PlayerManager.TotalPlastic.ToString();
            woodCountText.text = GameEventsManager.Instance.PlayerManager.TotalWood.ToString();
            metalCountText.text = GameEventsManager.Instance.PlayerManager.TotalMetal.ToString();

            plasticBottle.text = "x" + GameEventsManager.Instance.PlayerManager.WaterBottle.ToString();
            plasticTrash.text = "x" + GameEventsManager.Instance.PlayerManager.PlasticTrash.ToString();
            woodenCrate.text = "x" + GameEventsManager.Instance.PlayerManager.WoodenCrate.ToString();
            woodenPlank.text = "x" + GameEventsManager.Instance.PlayerManager.WoodenPlank.ToString();
        }
    }
}
