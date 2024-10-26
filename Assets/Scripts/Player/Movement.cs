using System;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float slideSpeed = 15f;
    private bool facingRight = true;
    private bool isMoving = false;
    public event Action<bool> IsRunning;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on parent");
        }
    }

    public void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isCurrentlyMoving = moveInput != 0;

        if (isCurrentlyMoving != isMoving)
        {
            isMoving = isCurrentlyMoving;
            IsRunning?.Invoke(isMoving);
        }

        if (isCurrentlyMoving)
        {
            if (moveInput > 0 && !facingRight) Flip();
            else if (moveInput < 0 && facingRight) Flip();
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.LeftShift)) Slide();
    }

    private void Slide()
    {
        rb.velocity = new Vector2(rb.velocity.x * slideSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Transform rootTransform = transform.root;
        Vector3 scaler = rootTransform.localScale;
        scaler.x *= -1;
        rootTransform.localScale = scaler;
    }
}
