using Events;
using Ink.Runtime;
using Input;
using UnityEngine;

namespace Dialog
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Ink Story")]
        [SerializeField] private TextAsset inkJson;

        private Story _story;
        private int _currentChoiceIndex = -1;

        private bool _dialoguePlaying;

        private InkExternalFunctions _inkExternalFunctions;
        private InkDialogueVariables _inkDialogueVariables;

        private void Awake() 
        {
            _story = new Story(inkJson.text);
            _inkExternalFunctions = new InkExternalFunctions();
            _inkExternalFunctions.Bind(_story);
            _inkDialogueVariables = new InkDialogueVariables(_story);
        }

        private void OnDestroy() 
        {
            _inkExternalFunctions.Unbind(_story);
        }

        private void OnEnable() 
        {
            GameEventsManager.Instance.DialogueEvents.OnEnterDialogue += EnterDialogue;
            GameEventsManager.Instance.InputEvents.OnSubmitPressed += SubmitPressed;
            GameEventsManager.Instance.DialogueEvents.OnUpdateChoiceIndex += UpdateChoiceIndex;
            GameEventsManager.Instance.DialogueEvents.OnUpdateInkDialogueVariable += UpdateInkDialogueVariable;
            GameEventsManager.Instance.QuestEvents.onQuestStateChange += QuestStateChange;
        }

        private void OnDisable() 
        {
            GameEventsManager.Instance.DialogueEvents.OnEnterDialogue -= EnterDialogue;
            GameEventsManager.Instance.InputEvents.OnSubmitPressed -= SubmitPressed;
            GameEventsManager.Instance.DialogueEvents.OnUpdateChoiceIndex -= UpdateChoiceIndex;
            GameEventsManager.Instance.DialogueEvents.OnUpdateInkDialogueVariable -= UpdateInkDialogueVariable;
            GameEventsManager.Instance.QuestEvents.onQuestStateChange -= QuestStateChange;
        }

        private void QuestStateChange(Quest quest) 
        {
            GameEventsManager.Instance.DialogueEvents.UpdateInkDialogueVariable(
                quest.info.id + "State",
                new StringValue(quest.state.ToString())
            );
        }

        private void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value) 
        {
            _inkDialogueVariables.UpdateVariableState(name, value);
        }

        private void UpdateChoiceIndex(int choiceIndex) 
        {
            _currentChoiceIndex = choiceIndex;
        }

        private void SubmitPressed(InputEventContext inputEventContext) 
        {
            // if the context isn't dialogue, we never want to register input here
            if (!inputEventContext.Equals(InputEventContext.Dialogue)) 
            {
                return;
            }

            ContinueOrExitStory();
        }

        private void EnterDialogue(string knotName) 
        {
            // don't enter dialogue if we've already entered
            if (_dialoguePlaying) 
            {
                return;
            }

            _dialoguePlaying = true;
            

            // inform other parts of our system that we've started dialogue
            
            
            //GameEventsManager.Instance.DialogueEvents.DialogueStarted();
            
            // freeze player movement
            GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
            
            // input event context
            GameEventsManager.Instance.InputEvents.ChangeInputEventContext(InputEventContext.Dialogue);
            
            // jump to the knot
            if (!knotName.Equals(""))
            {
                _story.ChoosePathString(knotName);
            }
            else 
            {
                Debug.LogWarning("Knot name was the empty string when entering dialogue.");
            }
            
            // // start listening for variables
            // _inkDialogueVariables.SyncVariablesAndStartListening(_story);
            
            // kick off the story
            ContinueOrExitStory();
        }

        private void ContinueOrExitStory() 
        {
            if (_story.canContinue)
            {
                string dialogueLine = _story.Continue();
                Debug.Log(dialogueLine);
            }
            else
            {
                ExitDialogue();
            }
            // // make a choice, if applicable
            // if (_story.currentChoices.Count > 0 && _currentChoiceIndex != -1)
            // {
            //     _story.ChooseChoiceIndex(_currentChoiceIndex);
            //     // reset choice index for next time
            //     _currentChoiceIndex = -1;
            // }
            //
            // if (_story.canContinue)
            // {
            //     string dialogueLine = _story.Continue();
            //
            //     // handle the case where there's an empty line of dialogue
            //     // by continuing until we get a line with content
            //     while (IsLineBlank(dialogueLine) && _story.canContinue) 
            //     {
            //         dialogueLine = _story.Continue();
            //     }
            //     // handle the case where the last line of dialogue is blank
            //     // (empty choice, external function, etc...)
            //     if (IsLineBlank(dialogueLine) && !_story.canContinue) 
            //     {
            //         ExitDialogue();
            //     }
            //     else 
            //     {
            //         GameEventsManager.Instance.DialogueEvents.DisplayDialogue(dialogueLine, _story.currentChoices);
            //     }
            // }
            // else if (_story.currentChoices.Count == 0)
            // {
            //     ExitDialogue();
            // }
        }

        private void ExitDialogue()
        {
            Debug.Log("Exiting Dialogue");
            _dialoguePlaying = false;

            // // inform other parts of our system that we've finished dialogue
            // GameEventsManager.Instance.DialogueEvents.DialogueFinished();
            //
            // let player move again
            GameEventsManager.Instance.PlayerEvents.EnablePlayerMovement();
            //
            //Input event context
            GameEventsManager.Instance.InputEvents.ChangeInputEventContext(InputEventContext.Default);
            //
            // // stop listening for dialogue variables
            // _inkDialogueVariables.StopListening(_story);

            // reset story state
            _story.ResetState();
        }

        private bool IsLineBlank(string dialogueLine)
        {
            return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
        }
    }
}