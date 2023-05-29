using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightController : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    public float speed = 20f;
    public float acceleration = 10f;
    public float stoppingDistance = 0.5f;

    public UnityEngine.Rendering.Universal.Light2D light2D;

    //StateMachineStuff
    MouseLightStateClass currentState;
    //States needed are: Follow state, wait state

    [HideInInspector] public MouseLightFollowState followState = new MouseLightFollowState();
    [HideInInspector] public MouseLightWaitState waitState = new MouseLightWaitState();


    private void Start()
    {
        currentState = followState;
        rb = GetComponent<Rigidbody2D>();
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
