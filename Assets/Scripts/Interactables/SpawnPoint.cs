using UnityEngine;

namespace Interactables
{
    public class SpawnPoint : MonoBehaviour
    {
        public Transform Transform;
        public bool IsOccupied;
        private GameObject _item;

        public SpawnPoint(Transform transform)
        {
            Transform = transform;
            IsOccupied = false;
        }

        private void Update()
        {
            if (!_item) IsOccupied = false;
        }

        public void SetGameObject(GameObject gO)
        {
            _item = gO;
        }
    }
}
