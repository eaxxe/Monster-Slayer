using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public event Action OnHealthChange;

    private void Start()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChange?.Invoke();
    }
}
