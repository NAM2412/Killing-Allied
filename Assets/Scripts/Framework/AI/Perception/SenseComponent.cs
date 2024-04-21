using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComponent : MonoBehaviour
{
    static List<PerceptionStimuli> registerStimulis = new List<PerceptionStimuli>();
    List<PerceptionStimuli> perceivableStimulis = new List<PerceptionStimuli>();
    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if (registerStimulis.Contains(stimuli))
            return;

        registerStimulis.Add(stimuli);
    }
    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registerStimulis.Remove(stimuli);
    }
    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);

    private void Update()
    {
        foreach (var stimuli in registerStimulis)
        {
            if (IsStimuliSensable(stimuli))
            {
                if (!perceivableStimulis.Contains(stimuli))
                {
                    perceivableStimulis.Add(stimuli);
                    Debug.Log($"I just sensed {stimuli.gameObject}");
                }
            }
            else
            {
                if (perceivableStimulis.Contains(stimuli))
                {
                    perceivableStimulis.Remove(stimuli);
                    Debug.Log($"I lost track of {stimuli.gameObject}");
                }
            }
        }
    }

    protected virtual void DrawDebug()
    {

    }
    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
