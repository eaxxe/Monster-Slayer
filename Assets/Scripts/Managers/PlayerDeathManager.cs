using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private static PlayerDeathManager instance;
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
        if (_playerHealth.CurrentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}