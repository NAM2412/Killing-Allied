using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Compositor : Node
{
    LinkedList<Node> children = new LinkedList<Node>();
    LinkedListNode<Node> currentChild = null;

    protected override NodeResult Execute()
    {
        if (children.Count == 0)
        {
            return NodeResult.Success;
        }

        currentChild = children.First;
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        return base.Update();
    }

    protected bool Next()
    {
        if(currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }
        return false;
    }

    protected override void End()
    {
        currentChild = null;
    }

    protected Node GetCurrentChild()
    {
        return currentChild.Value;
    }

    public void AddChild(Node newChild)
    {
        children.AddLast(newChild);
    }
}
