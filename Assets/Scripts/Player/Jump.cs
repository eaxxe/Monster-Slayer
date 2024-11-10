using System;
using UnityEngine;

public class VariableJump : MonoBehaviour, IJump
{
    //Component fields
    private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _layerMask;

    //general fields
    private float _jumpVelocity = 6.5f;
    private float _interruptingJump = 2f;
    private float _groundCheckRadius = 0.4f;
    public event Action<bool> OnJump;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        var jumpInput = Input.GetButtonDown("Jump");
        var jumpInputReleased = Input.GetButtonUp("Jump");

        if (jumpInput && IsGrounded())
        {
            OnJump?.Invoke(true);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpVelocity);
        }
        if (jumpInputReleased && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y / _interruptingJump);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _layerMask);
    }
}
