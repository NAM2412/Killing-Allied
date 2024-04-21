using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SenseComponent : MonoBehaviour
{
    [SerializeField] float forgettingTime = 3f;
    static List<PerceptionStimuli> registerStimulis = new List<PerceptionStimuli>();
    List<PerceptionStimuli> perceivableStimulis = new List<PerceptionStimuli>();

    Dictionary<PerceptionStimuli, Coroutine> ForgettingRoutines = new Dictionary<PerceptionStimuli, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool sucessullySensed);
    public event OnPerceptionUpdated onPerceptionUpdated;
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
                    if (ForgettingRoutines.TryGetValue(stimuli, out Coroutine routine))
                    {
                        StopCoroutine(routine);
                        ForgettingRoutines.Remove(stimuli);
                    }
                    else
                    {
                        onPerceptionUpdated?.Invoke(stimuli, true);
                    } 
                }
            }
            else
            {
                if (perceivableStimulis.Contains(stimuli))
                {
                    perceivableStimulis.Remove(stimuli);
                    ForgettingRoutines.Add(stimuli,StartCoroutine(ForgetStimuli(stimuli)));
                }
            }
        }
    }
    IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgettingTime);
        ForgettingRoutines.Remove(stimuli);
        onPerceptionUpdated?.Invoke(stimuli, false);
    }
    protected virtual void DrawDebug()
    {

    }
    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
