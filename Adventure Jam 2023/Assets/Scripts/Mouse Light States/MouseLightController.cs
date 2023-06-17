using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightController : MonoBehaviour
{
    public GameObject player;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    public float speed = 20f;
    public float acceleration = 10f;
    public float stoppingDistance = 0.5f;
    public float teleportDistance = 20f;
    public float waitLightStrength = 10f;
    public float followLightStrength = 6f;

    public UnityEngine.Rendering.Universal.Light2D light2D;
    public GameObject GFX;
    public float rotationAmount;

    //StateMachineStuff
    MouseLightStateClass currentState;
    //States needed are: Follow state, wait state

    [HideInInspector] public MouseLightInactiveState inactiveState = new MouseLightInactiveState();
    [HideInInspector] public MouseLightWakingState wakingState = new MouseLightWakingState();
    [HideInInspector] public MouseLightFollowState followState = new MouseLightFollowState();
    [HideInInspector] public MouseLightWaitState waitState = new MouseLightWaitState();

    private void Start()
    {
        currentState = inactiveState;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
