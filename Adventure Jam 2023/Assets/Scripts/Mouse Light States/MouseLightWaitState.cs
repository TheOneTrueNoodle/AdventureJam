using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightWaitState : MouseLightStateClass
{
    public override void EnterState(MouseLightController element)
    {
        element.waitLight.SetActive(true);
        element.followLight.SetActive(false);
    }
    public override void UpdateState(MouseLightController element)
    {
        if (Input.GetButtonDown("Light Action"))
        {
            element.SwitchState(element.followState);
        }
    }
}
