using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private Transform playerTransform;
    
    private Vector2 _movement;

    private bool _isSwimming;

    public int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isSwimming = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isSwimming)
        {
            Swim();
        }
        else
        {
            Walk();
        }
    }

    /// <summary>
    /// Works by using Unity's PlayerInput calls
    /// </summary>
    /// <param name="value"> value from controller </param>
    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    private void Swim()
    {
        rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * speed; //normalized to avoid greater speed in diagonal
        if (_movement.magnitude > 0.1f) //if input change rotation, if not keep old rotation as to not reset to 0
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            playerTransform.rotation = Quaternion.Euler(0, 0, angle); 
        }
    }

    private void Walk()
    {
        rb.linearVelocity = new Vector2(_movement.x, rb.linearVelocity.y).normalized * speed;
    }

    public void StartSwimming()
    {
        rb.linearVelocity = new Vector2(0, 0);
        _isSwimming = true;
        col.enabled = false;
        capsule.enabled = true;
        rb.gravityScale = 0f;
    }
}
