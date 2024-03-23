using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float maxHealth = 100;

    // change health event
    public delegate void OnHealthChange(float health, float delta, float maxHealth);
    public event OnHealthChange onHealthChange;

    // take damage event
    public delegate void OnTakeDamage(float health, float delta, float maxHealth);
    public event OnTakeDamage onTakeDamage;

    // on health == 0
    public delegate void OnHealthEmpty();
    public event OnHealthEmpty onHealthEmpty;

    public void ChangHealth(float amt)
    {
        if (amt == 0)
        {
            return;
        }

        health += amt;

        if (amt < 0)
        {
            onTakeDamage?.Invoke(health, amt, maxHealth);
        }
        
        onHealthChange?.Invoke(health, amt, maxHealth);

        if (health <= 0)
        {
            health = 0;
            onHealthEmpty?.Invoke();
        }

        Debug.Log($"{gameObject.name}, taking damage: {amt}, current health: {health}");
    }
}
