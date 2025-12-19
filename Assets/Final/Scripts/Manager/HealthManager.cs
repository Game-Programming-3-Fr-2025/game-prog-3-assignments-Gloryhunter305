using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;

    public event Action<GameObject> OnDeath;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (OnDeath != null)
        {
            OnDeath(gameObject);
        }
        Destroy(gameObject);
    }
}
