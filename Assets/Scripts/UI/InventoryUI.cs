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

        void OnEnable()
        {
            plasticCountText.text = GameEventsManager.Instance.PlayerManager.Inventory["Plastic"].ToString();
            woodCountText.text = GameEventsManager.Instance.PlayerManager.Inventory["Wood"].ToString();
            metalCountText.text = GameEventsManager.Instance.PlayerManager.Inventory["Metal"].ToString();
        }
    }
}
