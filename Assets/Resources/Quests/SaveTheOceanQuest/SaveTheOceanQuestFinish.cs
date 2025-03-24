using UnityEngine;

namespace Resources.Quests.SaveTheOceanQuest
{
    public class SaveTheOceanQuestFinish : MonoBehaviour
    {
        public void Start()
        {
            Debug.Log("Finished Save The Ocean");
            Destroy(gameObject);
        }
    }
}
