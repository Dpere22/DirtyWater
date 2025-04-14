using UnityEngine;
using UnityEngine.Serialization;

namespace Interactables
{
    public class SpawnPoint : MonoBehaviour
    {
        public Transform Transform;
        [FormerlySerializedAs("IsOccupied")] public bool isOccupied;
        private GameObject _item;
        public SpawnPoint(Transform transform)
        {
            Transform = transform;
            isOccupied = false;
        }

        private void Start()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr == null) return; //safety check
            sr.enabled = false;
        }

        private void Update()
        {
            if (!_item) isOccupied = false;
        }

        public void SetGameObject(GameObject gO)
        {
            _item = gO;
        }
    }
}
