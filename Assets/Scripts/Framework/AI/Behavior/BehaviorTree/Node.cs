using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeResult
{
    Success,
    Failure,
    Inprogress
}
public abstract class Node 
{
    public NodeResult UpdateNode()
    {
        //one off thing
        if(!started)
        {
            started = true;
            NodeResult executeResult = Execute();
            
            if (executeResult != NodeResult.Inprogress)
            {
                EndNode();
                return executeResult;
            }
        }

        //time base
        NodeResult updateResult = Update();
        if (updateResult != NodeResult.Inprogress)
        {
            EndNode();
        }
        return updateResult;
    }

    #region override in child class
    protected virtual NodeResult Update()
    {
        // time based
        return NodeResult.Success;
    }
    protected virtual NodeResult Execute()
    {
        //one off thing
        return NodeResult.Success;
    }
    protected virtual void End()
    {
        // reset and cleaned up
    }
    #endregion


    private void EndNode()
    {
        started = false;
        End();
    }

    public void Abort()
    {
        EndNode();
    }

    bool started = false;
    int priority;

    public int GetPriority()
    {
        return priority;
    }

    public virtual void SortPriority(ref int priorityConter) 
    {
        priority = priorityConter++;
        Debug.Log($"{this} has priority {priority}");
    }
}
