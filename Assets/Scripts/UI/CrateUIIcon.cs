using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CrateUIIcon : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite controllerIcon;
        [SerializeField] private Sprite keyboardIcon;

        private void Update()
        {
            image.sprite = ControllerInfo.ControllerName.Equals("Keyboard&Mouse") ? keyboardIcon : controllerIcon;
        }
    }
}
