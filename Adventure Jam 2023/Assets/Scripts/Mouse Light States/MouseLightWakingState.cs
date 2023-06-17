using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightWakingState : MouseLightStateClass
{
    private float t;
    private float minimum;
    private Vector2 currentVelocity = Vector2.zero;

    public override void EnterState(MouseLightController element)
    {
        element.anim.SetBool("isOn", true);
        minimum = 0;
        t = 0;
    }
    public override void UpdateState(MouseLightController element)
    {
        if (element.light2D.intensity < 1)
        {
            Vector2 direction = Vector2.up;
            Vector2 targetVelocity = direction * 0.5f;

            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, 0.1f * Time.fixedDeltaTime);


            Vector2 movement = currentVelocity * Time.fixedDeltaTime;

            element.rb.MovePosition(element.rb.position + movement);

            element.light2D.intensity = Mathf.Lerp(minimum, 1, t);
            t += Time.deltaTime;
        }
        else if(element.light2D.intensity >= 1)
        {
            element.light2D.intensity = 1f;
            element.player.GetComponent<PlayerMovement>().canMove = true;
            element.SwitchState(element.followState);
        }
    }
}
