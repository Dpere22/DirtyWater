using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Upgrades
{
    public abstract class Upgrade : MonoBehaviour
    {
        [SerializeField] protected Button button;
        [SerializeField] protected TextMeshProUGUI costText;
        public abstract void DoUpgrade();

        public abstract void EnableUpgrade();
    }
}
