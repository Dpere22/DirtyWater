using Events;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
    {
        [Header("Components")]
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI choiceText;

        private int _choiceIndex = -1;

        public void SetChoiceText(string choiceTextString)
        {
            choiceText.text = choiceTextString;
        }

        public void SetChoiceIndex(int choiceIndex)
        {
            this._choiceIndex = choiceIndex;
        }

        public void SelectButton()
        {
            button.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            GameEventsManager.Instance.DialogueEvents.UpdateChoiceIndex(_choiceIndex);
        }
    }
}