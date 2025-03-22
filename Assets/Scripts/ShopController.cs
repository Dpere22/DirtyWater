using UnityEngine;
using UnityEngine.EventSystems;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
