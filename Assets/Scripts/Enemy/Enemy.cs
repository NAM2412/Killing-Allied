using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComponent;
    [SerializeField] BehaviorTree behaviorTree;
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
            behaviorTree.Blackboard.SetOrAddData("Target", target);
        }    
        else
        {
            behaviorTree.Blackboard.RemoveBlackboardData("Target");
        }    
    }

    private void HealthComponent_TakenDamage(float health, float delta, float maxHealth, GameObject instigator)
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
        if (behaviorTree && behaviorTree.Blackboard.GetblackboardData("Target", out GameObject target))
        {
            Vector3 drawTargetPosition = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPosition, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPosition);
        }
    }
}
