using UnityEngine;

namespace Interactables
{
    [CreateAssetMenu(fileName = "GarbageInfoSO", menuName = "Scriptable Objects/GarbageInfoSO")]
    public class GarbageInfoSO : ScriptableObject
    {
        [SerializeField] public string garbageId;
        [SerializeField] public int weight;
        [SerializeField] public int plasticValue;
        [SerializeField] public int woodValue;
        [SerializeField] public int metalValue;

    }
}
