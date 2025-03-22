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

        // Update is called once per frame
        void Update()
        {
            plasticCountText.text = PlayerManager.inventory["Plastic"].ToString();
            woodCountText.text = PlayerManager.inventory["Wood"].ToString();
            metalCountText.text = PlayerManager.inventory["Metal"].ToString();
        }
    }
}
