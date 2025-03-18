using Events;
using Input;
using UnityEngine;

namespace QuestSystem
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class QuestPoint : MonoBehaviour
    {
        [Header("Dialogue (optional)")]
        [SerializeField] private string dialogueKnotName;

        [Header("Quest")]
        [SerializeField] private QuestInfoSO questInfoForPoint;

        [Header("Config")]
        [SerializeField] private bool startPoint = true;
        [SerializeField] private bool finishPoint = true;

        private bool _playerIsNear;
        private string _questId;
        private QuestState _currentQuestState;

        private QuestIcon _questIcon;


        private void Reset()
        {
            CircleCollider2D col = GetComponent<CircleCollider2D>();
            if (col != null)
            {
                col.isTrigger = true;
            }
        }
        private void Awake() 
        {
            _questId = questInfoForPoint.ID;
            _questIcon = GetComponentInChildren<QuestIcon>();
        }

        private void OnEnable()
        {
            if(GameEventsManager.Instance == null) Debug.LogError("Game Events Manager is null!");
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
            GameEventsManager.Instance.InputEvents.OnSubmitPressed += SubmitPressed;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
            GameEventsManager.Instance.InputEvents.OnSubmitPressed -= SubmitPressed;
        }

        private void SubmitPressed(InputEventContext inputEventContext)
        {
            if (!_playerIsNear || !inputEventContext.Equals(InputEventContext.Default))
            {
                return;
            }

            // if we have a knot name defined, try to start dialogue with it
            if (!dialogueKnotName.Equals("")) 
            {
                GameEventsManager.Instance.DialogueEvents.EnterDialogue(dialogueKnotName);
            }
            // otherwise, start or finish the quest immediately without dialogue
            else 
            {
                // start or finish a quest
                if (_currentQuestState.Equals(QuestState.CanStart) && startPoint)
                {
                    GameEventsManager.Instance.QuestEvents.StartQuest(_questId);
                }
                else if (_currentQuestState.Equals(QuestState.CanFinish) && finishPoint)
                {
                    GameEventsManager.Instance.QuestEvents.FinishQuest(_questId);
                }
            }
        }

        private void QuestStateChange(Quest quest)
        {
            // only update the quest state if this point has the corresponding quest
            if (quest.Info.ID.Equals(_questId))
            {
                _currentQuestState = quest.State;
                Debug.Log("Quest with id: " + _questId + "updated to state: " + _currentQuestState);
                _questIcon.SetState(_currentQuestState, startPoint, finishPoint);
            }
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = true;
            }
        }

        private void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = false;
            }
        }
    }
}