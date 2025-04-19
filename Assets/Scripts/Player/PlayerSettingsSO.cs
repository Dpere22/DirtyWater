using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "Scriptable Objects/PlayerSettingsSO")]
    public class PlayerSettingsSO : ScriptableObject
    {
        [SerializeField] public float maxTime;
        [SerializeField] public int maxWeight;
        [SerializeField] public int walkingSpeed;
        [SerializeField] public int swimmingSpeed;
    }
}
