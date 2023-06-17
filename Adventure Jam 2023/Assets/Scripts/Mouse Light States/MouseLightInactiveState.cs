using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightInactiveState : MouseLightStateClass
{
    public override void EnterState(MouseLightController element)
    {
        element.anim.SetBool("isOn", false);
    }
    public override void UpdateState(MouseLightController element)
    {
        
    }
}
