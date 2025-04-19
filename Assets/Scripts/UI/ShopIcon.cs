using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopIcon : MonoBehaviour
    {

        [SerializeField] private Image image;
        [SerializeField] private Sprite controllerIcon;
        [SerializeField] private Sprite keyboardIcon;

        private void OnEnable()
        {
            image.sprite = ControllerInfo.ControllerName.Equals("Keyboard&Mouse") ? keyboardIcon : controllerIcon;
        }
    }
}
