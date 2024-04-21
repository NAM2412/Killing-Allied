using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [SerializeField] SenseComponent[] senses;
    LinkedList<PerceptionStimuli> currentlyPerceivedStimulis = new LinkedList<PerceptionStimuli>();

    PerceptionStimuli targetStimuli;

    public delegate void OnPerceptionTargetChanged(GameObject target, bool sensed); 
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;
    void Start()
    {
        foreach (SenseComponent sense in senses)
        {
            sense.onPerceptionUpdated += Sense_onPerceptionUpdated;
        }
    }

    private void Sense_onPerceptionUpdated(PerceptionStimuli stimuli, bool sucessullySensed)
    {
        var nodeFound = currentlyPerceivedStimulis.Find(stimuli);
        if (sucessullySensed)
        {
            // if already has stimuli, add after the node found, if not having it, add last.
            if (nodeFound != null)
            {
                currentlyPerceivedStimulis.AddAfter(nodeFound, stimuli);
            }
            else
            {
                currentlyPerceivedStimulis.AddLast(stimuli);
            }
        }
        else
        {
            currentlyPerceivedStimulis.Remove(nodeFound);
        }

        if(currentlyPerceivedStimulis.Count != 0)
        {
            PerceptionStimuli highestStimuli = currentlyPerceivedStimulis.First.Value;
            if(targetStimuli == null || targetStimuli != highestStimuli)
            {
                targetStimuli = highestStimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, true);
            }
        }    
        else
        {
            if (targetStimuli != null)
            {
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, false);
                targetStimuli = null;
            }
        }
    }
}
