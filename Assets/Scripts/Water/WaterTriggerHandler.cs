using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _waterMask;
    [SerializeField] private GameObject splashParticles;

    private EdgeCollider2D _edgeColl;
    
    private InteractableWater _water;

    private void Awake()
    {
        _edgeColl = GetComponent<EdgeCollider2D>();
        _water = GetComponent<InteractableWater>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //This is broken idk why yet tho
        // Debug.Log("Entered");
        // int wmv = _waterMask.value;
        // Debug.Log("Entered with mask");
        // Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        // //Removed some layer mask stuff, may need to add layer if collisions break
        // if (rb != null)
        // {
        //     Debug.Log("Entered with rb");
        //     Vector2 localPos = gameObject.transform.localPosition;
        //     Vector2 hitObjectPos = other.transform.position;
        //     Bounds hitObjectBounds = other.bounds;
        //
        //     Vector3 spawnPos;
        //     if (other.transform.position.y >= _edgeColl.points[1].y + _edgeColl.offset.y + localPos.y)
        //     {
        //         //hit from above
        //         spawnPos = hitObjectPos - new Vector2(0f, hitObjectBounds.extents.y);
        //     }
        //     else
        //     {
        //         spawnPos = hitObjectPos + new Vector2(0f, hitObjectBounds.extents.y);
        //     }
        //     
        //     Instantiate(splashParticles, spawnPos, Quaternion.identity);
        //
        //     int multiplier = 1;
        //     if (rb.linearVelocity.y < 0)
        //     {
        //         multiplier = -1;
        //     }
        //     else
        //     {
        //         multiplier = 1;
        //     }
        //
        //     float vel = rb.linearVelocity.y * _water.ForceMultiplier;
        //     vel = Mathf.Clamp(Mathf.Abs(vel), 0f, _water.MaxForce);
        //     vel *= multiplier;
        //     
        //     _water.Splash(other, vel);
        // }
    }
}
