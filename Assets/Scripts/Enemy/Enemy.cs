using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComponent;

    GameObject target;
    void Start()
    {
        if (healthComponent != null)
        {
            healthComponent.onHealthEmpty += HealthComponent_StartDeath;
            healthComponent.onTakeDamage += HealthComponent_TakenDamage;
        }

        perceptionComponent.onPerceptionTargetChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
        if(sensed)
        {
            this.target = target;
        }    
        else
        {
            this.target = null;
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

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Vector3 drawTargetPosition = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPosition, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPosition);
        }
    }
}
