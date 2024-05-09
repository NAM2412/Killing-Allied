using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_MoveToTarget : Node
{
    NavMeshAgent agent;
    string targetKey;
    GameObject target;
    float acceptableDistance = 1f;
    BehaviorTree tree;

    public Task_MoveToTarget(BehaviorTree tree, string targetKey, float acceptableDistance = 1f)
    {
        this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;
        this.tree = tree;
    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = tree.Blackboard;
        if(blackboard == null || !blackboard.GetblackboardData(targetKey, out target))
        {
            return NodeResult.Failure;
        }

        agent = tree.GetComponent<NavMeshAgent>();
        if(agent == null)
        {
            return NodeResult.Failure;
        }

        if(IsTargetInAcceptableDistance())
        {
            return NodeResult.Success;
        }

        blackboard.onBlackboardValueChange += BlackboardValueChange;

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        return NodeResult.Inprogress; 
    }

    private void BlackboardValueChange(string key, object val)
    {
        if(key == targetKey)
        {
            target = (GameObject)val;
        }
    }


    protected override NodeResult Update()
    {
        if(target == null)
        {
            agent.isStopped = true;
            return NodeResult.Failure;
        }

        agent.SetDestination(target.transform.position);
        if(IsTargetInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.Inprogress;
    }
    private bool IsTargetInAcceptableDistance()
    {
        return Vector3.Distance(target.transform.position, tree.transform.position) <= acceptableDistance;
    }

}
