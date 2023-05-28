using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightWaitState : MouseLightStateClass
{
    private float t;
    private float minimum;

    public override void EnterState(MouseLightController element)
    {
        minimum = element.light2D.pointLightOuterRadius;
        t = 0;
    }
    public override void UpdateState(MouseLightController element)
    {
        if(element.light2D.pointLightOuterRadius != 6)
        {
            element.light2D.pointLightOuterRadius = Mathf.Lerp(minimum, 6, t);
            t += 2f * Time.deltaTime;
        }

        if (Input.GetButtonDown("Light Action"))
        {
            element.SwitchState(element.followState);
        }
    }
}
