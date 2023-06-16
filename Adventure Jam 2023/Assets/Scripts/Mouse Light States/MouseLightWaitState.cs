using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightWaitState : MouseLightStateClass
{
    private float t;
    private float minimum;

    public override void EnterState(MouseLightController element)
    {
        element.GFX.transform.rotation = Quaternion.Euler(Vector3.zero);
        element.anim.SetBool("platformOpen", true);
        element.gameObject.layer = LayerMask.NameToLayer("Ground");
        element.rb.bodyType = RigidbodyType2D.Kinematic;
        minimum = element.light2D.pointLightOuterRadius;
        t = 0;
    }
    public override void UpdateState(MouseLightController element)
    {
        if(element.light2D.pointLightOuterRadius != element.waitLightStrength)
        {
            element.light2D.pointLightOuterRadius = Mathf.Lerp(minimum, element.waitLightStrength, t);
            t += 2f * Time.deltaTime;
        }

        if (Input.GetButtonDown("Light Action"))
        {
            element.SwitchState(element.followState);
        }
    }
}
