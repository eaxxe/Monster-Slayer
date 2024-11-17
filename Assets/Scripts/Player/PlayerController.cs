using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovement movement;
    private IJump jump;
    private IAttack attack;
    private IDash dash;
    private FallDamage fallDamage;

    void Start()
    {
        movement = GetComponent<IMovement>();
        jump = GetComponent<IJump>();
        attack = GetComponent<IAttack>();
        dash = GetComponent<IDash>();
        fallDamage = GetComponent<FallDamage>();
    }

    void Update()
    {
        movement.HandleMovement();
        jump.Jump();
        attack.HandleAttack();
        dash.Dash();
        fallDamage.DamageFall();
    }
}
