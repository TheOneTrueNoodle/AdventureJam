using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightController : MonoBehaviour
{
    public float speed = 20f;

    public GameObject followLight;
    public GameObject waitLight;

    //StateMachineStuff
    MouseLightStateClass currentState;
    //States needed are: Follow state, wait state

    [HideInInspector] public MouseLightFollowState followState = new MouseLightFollowState();
    [HideInInspector] public MouseLightWaitState waitState = new MouseLightWaitState();


    private void Start()
    {
        currentState = followState;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(MouseLightStateClass state)
    {
        currentState = state;
        if (state == null)
        {
            currentState = waitState;
        }
        currentState.EnterState(this);
    }
}
