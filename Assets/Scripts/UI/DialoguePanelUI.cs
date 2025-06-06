using System.Collections.Generic;
using Events;
using Ink.Runtime;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DialoguePanelUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject contentParent;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private DialogueChoiceButton[] choiceButtons;

        private void Awake()
        {
            contentParent.SetActive(false);
            ResetPanel();
        }

        private void OnEnable()
        {
            GameEventsManager.Instance.DialogueEvents.OnDialogueStarted += DialogueStarted;
            GameEventsManager.Instance.DialogueEvents.OnDialogueFinished += DialogueFinished;
            GameEventsManager.Instance.DialogueEvents.OnDisplayDialogue += DisplayDialogue;
            GameEventsManager.Instance.DayEvents.OnDayEnd += DialogueFinished;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.DialogueEvents.OnDialogueStarted -= DialogueStarted;
            GameEventsManager.Instance.DialogueEvents.OnDialogueFinished -= DialogueFinished;
            GameEventsManager.Instance.DialogueEvents.OnDisplayDialogue -= DisplayDialogue;
        }

        private void DialogueStarted()
        {
            contentParent.SetActive(true);
        }

        private void DialogueFinished()
        {
            contentParent.SetActive(false);

            // reset anything for next time
            ResetPanel();
        }

        private void DisplayDialogue(string dialogueLine ,List<Choice> dialogueChoices)
        {
            dialogueText.text = dialogueLine;

            // defensive check - if there are more choices coming in than we can support, log an error
            if (dialogueChoices.Count > choiceButtons.Length) 
            {
                Debug.LogError("More dialogue choices ("
                               + dialogueChoices.Count + ") came through than are supported ("
                               + choiceButtons.Length + ").");
            }
            //
            // start with all the choice buttons hidden
            foreach (DialogueChoiceButton choiceButton in choiceButtons) 
            {
                choiceButton.gameObject.SetActive(false);
            }
        
            // enable and set info for buttons depending on ink choice information
            int choiceButtonIndex = dialogueChoices.Count - 1;
            for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
            {
                Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
                DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];
        
                choiceButton.gameObject.SetActive(true);
                choiceButton.SetChoiceText(dialogueChoice.text);
                choiceButton.SetChoiceIndex(inkChoiceIndex);
        
                if (inkChoiceIndex == 0)
                {
                    choiceButton.SelectButton();
                    GameEventsManager.Instance.DialogueEvents.UpdateChoiceIndex(inkChoiceIndex);
                }
        
                choiceButtonIndex--;
            }
        }

        private void ResetPanel()
        {
            dialogueText.text = "";
        }
    }
}