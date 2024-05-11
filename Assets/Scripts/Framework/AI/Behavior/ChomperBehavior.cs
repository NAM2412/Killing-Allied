using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out Node rootNode)
    {
        Selector rootSelector = new Selector();
        Sequencer attackTargetSeq = new Sequencer();
        Task_MoveToTarget moveToTarget = new Task_MoveToTarget(this, "Target",2);
        attackTargetSeq.AddChild(moveToTarget);

        BlackboardDecorator attackTargetDecorator = new BlackboardDecorator(this,
                                                                            attackTargetSeq, "Target",
                                                                            BlackboardDecorator.RunCondition.KeyExists,
                                                                            BlackboardDecorator.NotifyRule.RunConditionChange,
                                                                            BlackboardDecorator.NotifyAbort.both);

        rootSelector.AddChild(attackTargetDecorator);

        Sequencer patrollingSequence = new Sequencer();

        Task_GetNextPatrolPoint getNextPatrolPoint = new Task_GetNextPatrolPoint(this, "PatrolPoint");
        Task_MoveToLocation moveToPatrolPoint = new Task_MoveToLocation(this, "PatrolPoint", 3);
        Task_Wait waitAtPatrolPoint = new Task_Wait(2f);

        patrollingSequence.AddChild(getNextPatrolPoint);
        patrollingSequence.AddChild(moveToPatrolPoint);
        patrollingSequence.AddChild(waitAtPatrolPoint);
        rootSelector.AddChild(patrollingSequence);

        rootNode = rootSelector;
    }
}
