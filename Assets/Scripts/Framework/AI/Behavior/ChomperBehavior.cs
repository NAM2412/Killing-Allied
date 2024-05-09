using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out Node rootNode)
    {
        Task_MoveToTarget moveToTarget = new Task_MoveToTarget(this, "Target", 2f);

        rootNode = moveToTarget;
    }
}
