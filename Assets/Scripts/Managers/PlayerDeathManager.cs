using System;
using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private static PlayerDeathManager instance;
    public event Action PlayerDeath;
    private int count = 1;
    public static PlayerDeathManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PlayerDeathManager>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject("PlayerDeathManager");
                    instance = gameObject.AddComponent<PlayerDeathManager>();
                    DontDestroyOnLoad(gameObject);
                }

            }
            return instance;
        }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _playerHealth = FindObjectOfType<PlayerHealth>();
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChange += HealthChanged;
        }
        else
        {
            Debug.LogError("PlayerHealth component not found in the scene.");
        }
    }

    private void HealthChanged()
    {
        HandlePlayerDeath();
    }

    private void HandlePlayerDeath()
    {
        if (_playerHealth.CurrentHealth <= 0 && count == 1) 
        {
            PlayerDeath?.Invoke();
            count++;
            StartCoroutine(ReloadScene());
        }
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}