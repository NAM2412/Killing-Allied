using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Log : Node
{
    string message;
    public Task_Log(string message)
    {
        this.message = message;
    }
    protected override NodeResult Execute()
    {
        //Debug.Log(message);
        return NodeResult.Success;
    }
}
