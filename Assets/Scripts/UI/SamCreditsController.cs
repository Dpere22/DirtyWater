using UnityEngine;

namespace UI
{
    public class SamCreditsController : MonoBehaviour
    {
        void Start()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Vector2 velocity = new Vector2(3, 0);
            rb.linearVelocity = velocity;
        }
    }
}
