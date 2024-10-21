using UnityEngine;

public class HealthObserver : MonoBehaviour, IObserver
{
    public void HealthChanged()
    {
        Debug.Log("Player health changed!");
    }
}
