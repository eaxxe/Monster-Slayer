using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Movement _movement;
    private VariableJump _jump;
    private Attack _attack;
    private Animator _animator;
    private PlayerDeathManager _deathManager;
    private const string _IS_RUNNING = "IsRunning";
    private const string _IS_JUMP = "IsJump";
    private Rigidbody2D _rigidbody2D;
    private bool _isFall = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInParent<Animator>();
        _movement = FindAnyObjectByType<Movement>();
        if (_movement != null)
            _movement.IsRunning += PlayerIsRunning;
        _jump = FindAnyObjectByType<VariableJump>();
        if(_jump != null)
            _jump.OnJump += PlayerIsJump;
        _attack = FindAnyObjectByType<Attack>();
        if (_attack != null)
            _attack.Attacked += PlayerAttacked;
        _deathManager = FindAnyObjectByType<PlayerDeathManager>();
        if (_deathManager != null)
            _deathManager.PlayerDeath += PlayerDeath;
    }


    private void Update()
    {
        _animator.SetBool("IsGround", _jump.IsGrounded());
        Vector3 vector3 = _rigidbody2D.velocity.normalized;
        if (vector3.y < 0 && !_jump.IsGrounded() && !_isFall)
        {
            _animator.SetBool(_IS_JUMP, false);
            _animator.SetBool("IsFall", true);
            _isFall = true;
        }
        else
        {
            _animator.SetBool("IsFall", false);
            _isFall = false;
        }
        
    }
    private void PlayerIsRunning(bool isRunning)
    {
        _animator.SetBool(_IS_RUNNING, isRunning);
    }

    private void PlayerIsJump(bool isJump)
    {
        _animator.SetBool(_IS_JUMP, isJump);
    }

    private void PlayerAttacked(int which)
    {
        switch (which) {
            case 1:
                _animator.SetTrigger("IsAttack");
                break;
            case 2:
                _animator.SetTrigger("IsAttack1");
                break;
            case 3:
                _animator.SetTrigger("IsAttack2");
                break;
        }
    }

    private void PlayerDeath() {
        _animator.SetTrigger("IsDeath");
    }
}
