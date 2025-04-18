using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int speed;
    void Start()
    {
        rb.linearVelocity = new Vector2(speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cloud"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX * -1, 0);
        }
    }
}
