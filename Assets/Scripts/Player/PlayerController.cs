using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovement movement;
    private IJump jump;
    private IAttack attack;

    void Start()
    {
        movement = GetComponent<IMovement>();
        jump = GetComponent<IJump>();
        attack = GetComponent<IAttack>();
    }

    void Update()
    {
        movement.HandleMovement();
        jump.HandleJump();
        attack.HandleAttack();
    }
}
