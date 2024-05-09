using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Task_MoveToLocation : Node
{
    NavMeshAgent agent;
    string locationKey;
    Vector3 location;
    float acceptableDistance = 1f;
    BehaviorTree tree;

    public Task_MoveToLocation(BehaviorTree tree, string locationKey, float acceptableDistance = 1f)
    {
        this.tree = tree;
        this.locationKey = locationKey;
        this.acceptableDistance = acceptableDistance;
    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = tree.Blackboard;
        if (blackboard == null || !blackboard.GetblackboardData(locationKey, out location))
        {
            return NodeResult.Failure;
        }

        agent = tree.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            return NodeResult.Failure;
        }

        if (IsLocationInAcceptableDistance())
        {
            return NodeResult.Success;
        }

        agent.SetDestination(location);
        agent.isStopped = false;
        return NodeResult.Inprogress;

    }

    protected override NodeResult Update()
    {
        if(IsLocationInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }

        return NodeResult.Inprogress;
    }
    private bool IsLocationInAcceptableDistance()
    {
        return Vector3.Distance(location, tree.transform.position) <= acceptableDistance;
    }
}
