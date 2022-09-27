using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableThing : MonoBehaviour
{
    public int _maxHealth;
    public int _currentHealth;
    public HealthBar _healthBar;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }    

    public void Damage(int damage)
    {  
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);        
    }

    public void AddHealth(int health)
    {
        if(health + _currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += health;
        }
        _healthBar.SetHealth(_currentHealth);
    }

    public bool IsDead()
    {
        if(_currentHealth <= 0)        
            return true;
        
        return false;
    }
}
