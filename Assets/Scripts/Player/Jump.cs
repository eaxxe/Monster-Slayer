using UnityEngine;

public class Jump : MonoBehaviour, IJump
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}