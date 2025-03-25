using System.Collections.Generic;
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
        public List<QuestInfoSO> questInfoForPoint;
        private Queue<QuestInfoSO> _questInfoQueue = new();

        [Header("Config")]
        [SerializeField] private GameObject questManager;
        private QuestManager _questManager;

        [SerializeField] private GameObject interactIcon;

        private bool _playerIsNear;
        private string _questId = "";
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
            _questInfoQueue = new Queue<QuestInfoSO>(questInfoForPoint);
            _questId = _questInfoQueue.Dequeue().ID;
            _questIcon = GetComponentInChildren<QuestIcon>();
            _questManager = questManager.GetComponent<QuestManager>();
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
                if (_currentQuestState.Equals(QuestState.CAN_START))
                {
                    GameEventsManager.Instance.QuestEvents.StartQuest(_questId);
                }
                else if (_currentQuestState.Equals(QuestState.CAN_FINISH))
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
                _questIcon.SetState(_currentQuestState);
            }
            
            //working on this code.
            if (quest.State == QuestState.FINISHED && _questInfoQueue.Count > 0)
            {
                _questId = _questInfoQueue.Dequeue().ID;
                Quest q = _questManager.GetNewQuestById(_questId);
                _currentQuestState = q.State;
                _questIcon.SetState(_currentQuestState);
            }
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = true;
                interactIcon.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = false;
                interactIcon.SetActive(false);
            }
        }
    }
}