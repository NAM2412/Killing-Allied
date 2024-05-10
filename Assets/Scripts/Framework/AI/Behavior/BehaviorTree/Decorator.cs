using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : Node
{
    Node child;

    protected Node GetChild()
    {
        return child;
    }

    public Decorator(Node child)
    {
        this.child = child;
    }


}
