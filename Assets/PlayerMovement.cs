using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private Transform playerTransform;

    private bool _isSwimming;

    public int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isSwimming = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Works by using Unity's PlayerInput calls
    /// </summary>
    /// <param name="value"> value from controller </param>
    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        if (_isSwimming)
        {
            Swim(v);
        }
        else
        {
            Walk(v);
        }
    }

    private void Swim(Vector2 inputDirection)
    {
        rb.linearVelocity = new Vector2(inputDirection.x, inputDirection.y).normalized * speed;
        float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;

        // Apply rotation to the head
        playerTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Walk(Vector2 inputDirection)
    {
        rb.linearVelocity = new Vector2(inputDirection.x, rb.linearVelocity.y).normalized * speed;
    }

    public void StartSwimming()
    {
        Debug.Log("StartSwimming");
        _isSwimming = true;
        col.enabled = false;
        capsule.enabled = true;
        rb.gravityScale = 0f;
    }
}
