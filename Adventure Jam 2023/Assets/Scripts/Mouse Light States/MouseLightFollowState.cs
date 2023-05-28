using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightFollowState : MouseLightStateClass
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

        if (element.light2D.pointLightOuterRadius != 4)
        {
            element.light2D.pointLightOuterRadius = Mathf.Lerp(minimum, 4, t);
            t += 2f * Time.deltaTime;
        }

        if (Input.GetButtonDown("Light Action"))
        {
            element.SwitchState(element.waitState);
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(element.transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        element.transform.position = Vector3.MoveTowards(element.transform.position, targetPos, element.speed * Time.deltaTime);

        if (Vector3.Distance(targetPos, element.transform.position) < 1)
        {
            element.gameObject.transform.position = targetPos;
        }
    }
}
