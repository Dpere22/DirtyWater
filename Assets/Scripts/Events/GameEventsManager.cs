using UnityEngine;

namespace Events
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager Instance { get; private set; }

        public DialogueEvents DialogueEvents;
        public InputEvents InputEvents;
        public QuestEvents QuestEvents;

        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }
            Instance = this;
            //DialogueEvents = new DialogueEvents();
            InputEvents = new InputEvents();
            //QuestEvents = new QuestEvents();
        }
    }
}
