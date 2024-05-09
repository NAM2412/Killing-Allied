using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out Node rootNode)
    {
        Sequencer patrollingSequence = new Sequencer();

        Task_GetNextPatrolPoint getNextPatrolPoint = new Task_GetNextPatrolPoint(this, "PatrolPoint");
        Task_MoveToLocation moveToPatrolPoint = new Task_MoveToLocation(this, "PatrolPoint", 3);
        Task_Wait waitAtPatrolPoint = new Task_Wait(2f);

        patrollingSequence.AddChild(getNextPatrolPoint);
        patrollingSequence.AddChild(moveToPatrolPoint);
        patrollingSequence.AddChild(waitAtPatrolPoint);

        rootNode = patrollingSequence;
    }
}
