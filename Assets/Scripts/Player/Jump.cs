using System;
using UnityEngine;

public class Jump : MonoBehaviour, IJump
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxHorizontalSpeed = 2f; // §®§Ñ§Ü§ã§Ú§Þ§Ñ§Ý§î§ß§Ñ§ñ §ã§Ü§à§â§à§ã§ä§î §á§à §Ô§à§â§Ú§Ù§à§ß§ä§Ñ§Ý§Ú §Ó§à §Ó§â§Ö§Þ§ñ §á§â§í§Ø§Ü§Ñ

    private Rigidbody2D rb;
    public event Action<bool> OnJump;
    private bool isGrounded;
    private bool wasGrounded;
    private bool isFalling;
    private float previousYPosition; // §¥§Ý§ñ §à§ä§ã§Ý§Ö§Ø§Ú§Ó§Ñ§ß§Ú§ñ §á§â§Ö§Õ§í§Õ§å§ë§Ö§Û §á§à§Ù§Ú§è§Ú§Ú y

    public bool IsGrounded => isGrounded;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        previousYPosition = transform.position.y; // §ª§ß§Ú§è§Ú§Ñ§Ý§Ú§Ù§Ñ§è§Ú§ñ §á§â§Ö§Õ§í§Õ§å§ë§Ö§Û §á§à§Ù§Ú§è§Ú§Ú y
    }

    void Update()
    {
        HandleJump();
    }

    public void HandleJump()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        float currentYPosition = transform.position.y;

        if (currentYPosition < previousYPosition && rb.velocity.y < 0)
        {
            //Debug.Log("§Á §á§Ñ§Õ§Ñ§ð");
            isFalling = true;
        }
        previousYPosition = currentYPosition; // §°§Ò§ß§à§Ó§Ý§Ö§ß§Ú§Ö §á§â§Ö§Õ§í§Õ§å§ë§Ö§Û §á§à§Ù§Ú§è§Ú§Ú

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            OnJump?.Invoke(true);
        }

        // §°§Ô§â§Ñ§ß§Ú§é§Ú§Ó§Ñ§Ö§Þ §Ô§à§â§Ú§Ù§à§ß§ä§Ñ§Ý§î§ß§å§ð §ã§Ü§à§â§à§ã§ä§î §Ó§à §Ó§â§Ö§Þ§ñ §á§â§í§Ø§Ü§Ñ
        if (!isGrounded)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed), rb.velocity.y);
        }
    }
}
