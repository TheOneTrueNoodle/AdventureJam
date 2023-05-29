using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightRayGoal : MonoBehaviour
{
    private bool Activated = false;
    public UnityEvent ActivateAction;

    public void Activate()
    {
        if (Activated) { return; }

        Activated = true;
        ActivateAction.Invoke();
    }
}
