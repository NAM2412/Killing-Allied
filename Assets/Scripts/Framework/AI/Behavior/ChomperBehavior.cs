using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    protected override void ConstructTree(out Node rootNode)
    { 
        Task_Wait waitTask = new Task_Wait(2f);
        Task_Log log = new Task_Log("Logging");

        Sequencer root = new Sequencer();
        root.AddChild(log);
        root.AddChild(waitTask);

        rootNode = root;
    }
}
