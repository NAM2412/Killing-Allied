using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    void Start()
    {
        if (healthComponent != null)
        {
            healthComponent.onHealthEmpty += HealthComponent_StartDeath;
            healthComponent.onTakeDamage += HealthComponent_TakenDamage;
        }
    }

    private void HealthComponent_TakenDamage(float health, float delta, float maxHealth)
    {
        
    }

    private void HealthComponent_StartDeath()
    {
        TriggerDeathAnimation();
    }

    private void TriggerDeathAnimation()
    {
        Debug.Log(animator);
        if (animator != null)
        {
            animator.SetTrigger("Dead");
            
        }
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }
}
