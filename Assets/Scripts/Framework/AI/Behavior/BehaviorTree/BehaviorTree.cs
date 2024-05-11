using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    Node root;
    Blackboard blackboard = new Blackboard();

    public Blackboard Blackboard
    {
        get
        {
            return blackboard;
        }
    }
    private void Start()
    {
        ConstructTree(out root);
        SortTree();
    }

    private void SortTree()
    {
        int priorityCounter = 0;
        root.SortPriority(ref priorityCounter);

    }

    protected abstract void ConstructTree(out Node rootNode);

    private void Update()
    {
        root.UpdateNode();
    }
}
