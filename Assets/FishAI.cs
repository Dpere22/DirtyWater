using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float speed = 2f;
    public float directionChangeInterval = 2f;
    private Vector2 _moveDirection;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        PickNewDirection();
        InvokeRepeating(nameof(PickNewDirection), directionChangeInterval, directionChangeInterval);
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * (speed * Time.fixedDeltaTime));
        if (_moveDirection.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(_moveDirection.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);
        _moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect off terrain
        if (collision.collider.CompareTag("Ground"))
        {
            Vector2 normal = collision.contacts[0].normal;
            _moveDirection = Vector2.Reflect(_moveDirection, normal);
        }
    }
}
