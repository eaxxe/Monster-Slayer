using System;
using UnityEngine;

public class Attack : MonoBehaviour, IAttack
{
    [SerializeField, Range(10, 50)] private int damage = 40;
    [SerializeField, Range(0.5f, 1.5f)] private float attackRange = 1f;
    [SerializeField, Range(0.3f,1.5f)] private float attackCooldown = 0.4f;
    private byte _counterOfAttackCombo = 1;
    private float _timeBeforeLastAttackCombo;
    public event Action<int> Attacked;
    private float nextAttackTime = 0f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;

    public void HandleAttack()
    {
        _timeBeforeLastAttackCombo += Time.deltaTime;
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_counterOfAttackCombo > 3 || _timeBeforeLastAttackCombo > 1.5f) _counterOfAttackCombo = 1;
            Attacked?.Invoke(_counterOfAttackCombo);
            AttackEnemy();
            nextAttackTime = Time.time + attackCooldown;
            _counterOfAttackCombo++;
            _timeBeforeLastAttackCombo = 0;
        }
    }

    private void AttackEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, attackRange, _enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log($"We hit {enemy.name}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRange);
    }
}
