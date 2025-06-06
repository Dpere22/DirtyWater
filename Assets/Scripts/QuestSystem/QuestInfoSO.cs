using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
    public class QuestInfoSO : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }

        [Header("General")]
        public string displayName;

        [Header("Requirements")]
        public QuestInfoSO[] questPrerequisites;

        [Header("Steps")]
        public GameObject[] questStepPrefabs;

        [Header("RewardPrefab")] public GameObject rewardPrefab;

        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
            #if UNITY_EDITOR
            ID = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}