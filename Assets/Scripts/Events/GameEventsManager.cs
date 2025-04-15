using UnityEngine;

namespace Events
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager Instance { get; private set; }

        public DialogueEvents DialogueEvents;
        public InputEvents InputEvents;
        public QuestEvents QuestEvents;
        public PlayerEvents PlayerEvents;
        public DayEvents DayEvents;
        public ShopEvents ShopEvents;
        public PauseEvents PauseEvents;
        public TrashManager TrashManager; //This code is a bit odd here but will work for now.
        public PlayerManager PlayerManager; //same here
        public SoundEvents SoundEvents;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }
            Instance = this;
            DialogueEvents = new DialogueEvents();
            InputEvents = new InputEvents();
            QuestEvents = new QuestEvents();
            PlayerEvents = new PlayerEvents();
            DayEvents = new DayEvents();
            ShopEvents = new ShopEvents();
            PauseEvents = new PauseEvents();
            TrashManager = new TrashManager();
            PlayerManager = new PlayerManager();
            SoundEvents = new SoundEvents();
        }
    }
}
