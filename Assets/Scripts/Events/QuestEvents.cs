using System;
using QuestSystem;

namespace Events
{
    public class QuestEvents
    {
        public event Action<string> OnStartQuest;
        public void StartQuest(string id)
        {
            if (OnStartQuest != null)
            {
                OnStartQuest(id);
            }
        }

        public event Action<string> OnAdvanceQuest;
        public void AdvanceQuest(string id)
        {
            if (OnAdvanceQuest != null)
            {
                OnAdvanceQuest(id);
            }
        }
        
        public event Action<string> OnEnableQuest;

        public void EnableQuest(string id)
        {
            OnEnableQuest?.Invoke(id);
        }

        public event Action<string> OnFinishQuest;
        public void FinishQuest(string id)
        {
            if (OnFinishQuest != null)
            {
                OnFinishQuest(id);
            }
        }

        public event Action<Quest> OnQuestStateChange;
        public void QuestStateChange(Quest quest)
        {
            if (OnQuestStateChange != null)
            {
                OnQuestStateChange(quest);
            }
        }

        public event Action OnFinishGame;

        public void FinishGame()
        {
            OnFinishGame?.Invoke();
        }

        public event Action OnGetToolbox;

        public void GetToolbox()
        {
            OnGetToolbox?.Invoke();
        }

        public event Action OnGetChest;

        public void GetChest()
        {
            OnGetChest?.Invoke();
        }

        public event Action OnActivateToolbox;

        public void ActivateToolbox()
        {
            OnActivateToolbox?.Invoke();
        }

        public event Action OnActivateChest;

        public void ActivateChest()
        {
            OnActivateChest?.Invoke();
        }

        public event Action OnUpgradeShop;

        public void UpgradeShop()
        {
            OnUpgradeShop?.Invoke();
        }

    }
}