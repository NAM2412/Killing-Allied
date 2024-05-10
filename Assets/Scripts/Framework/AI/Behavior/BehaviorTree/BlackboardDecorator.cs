using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardDecorator : Decorator
{
    #region Enum
    public enum RunCondition
    {
        KeyExists, 
        KetNotExists
    }
    public enum NotifyRule
    {
        RunConditionChange,
        KeyValueChange
    }
    public enum NotifyAbort
    {
        none,
        self,
        lower,
        both
    }
    #endregion

    BehaviorTree tree;
    string key;

    RunCondition runCondition;
    NotifyRule notifyRule;
    NotifyAbort notifyAbort;

    public BlackboardDecorator(BehaviorTree tree, 
                                Node child, 
                                string key, 
                                RunCondition runCondition, 
                                NotifyRule notifyRule, 
                                NotifyAbort notifyAbort) : base(child)
    {
        this.tree = tree;
        this.key = key;
        this.runCondition = runCondition;
        this.notifyRule = notifyRule;
        this.notifyAbort = notifyAbort;
    }

    protected override NodeResult Execute()
    {
        Blackboard blackboard = tree.Blackboard;

        if (blackboard == null) return NodeResult.Failure;

        blackboard.onBlackboardValueChange += CheckNotify;
        if(CheckRunCondition())
        {
            return NodeResult.Inprogress;
        }
        else
        {
            return NodeResult.Failure;
        }
    }

    private bool CheckRunCondition()
    {
        throw new NotImplementedException();
    }

    private void CheckNotify(string key, object val)
    {
        
    }

    protected override NodeResult Update()
    {
        return GetChild().UpdateNode();
    }
}
