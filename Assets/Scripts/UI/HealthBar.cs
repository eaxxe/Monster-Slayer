using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private PlayerHealth _playerHealth;

    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("Slider component not found on " + gameObject.name);
            return;
        }

        _playerHealth = FindObjectOfType<PlayerHealth>();
        if (_playerHealth != null)
        {
            slider.maxValue = _playerHealth.MaxHealth; 
            slider.value = _playerHealth.CurrentHealth;
            _playerHealth.OnHealthChange += UpdateHealthBar;
        }
        else
        {
            Debug.LogError("PlayerHealth component not found in the scene.");
        }
    }

    void UpdateHealthBar()
    {
        slider.value = _playerHealth.CurrentHealth;
    }
}
