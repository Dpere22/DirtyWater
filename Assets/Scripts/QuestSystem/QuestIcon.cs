using UnityEngine;

namespace QuestSystem
{
    public class QuestIcon : MonoBehaviour
    {
        [Header("Icons")]
        [SerializeField] private GameObject requirementsNotMetToStartIcon;
        [SerializeField] private GameObject canStartIcon;
        [SerializeField] private GameObject requirementsNotMetToFinishIcon;
        [SerializeField] private GameObject canFinishIcon;

        public void SetState(QuestState newState)
        {
            // set all to inactive
            requirementsNotMetToStartIcon.SetActive(false);
            canStartIcon.SetActive(false);
            requirementsNotMetToFinishIcon.SetActive(false);
            canFinishIcon.SetActive(false);

            // set the appropriate one to active based on the new state
            switch (newState)
            {
                case QuestState.REQUIREMENTS_NOT_MET:
                    requirementsNotMetToStartIcon.SetActive(true);
                    break;
                case QuestState.CAN_START:
                    canStartIcon.SetActive(true);
                    break;
                case QuestState.IN_PROGRESS:
                    { requirementsNotMetToFinishIcon.SetActive(true); }
                    break;
                case QuestState.CAN_FINISH:
                    { canFinishIcon.SetActive(true); }
                    break;
                case QuestState.FINISHED:
                    break;
                default:
                    Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                    break;
            }
        }
    }
}