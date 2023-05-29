using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLightFollowState : MouseLightStateClass
{
    private float t;
    private float minimum;
    private Vector2 currentVelocity = Vector2.zero;
    private bool isLocked = false;
    private Vector2 lockOffset;

    public override void EnterState(MouseLightController element)
    {
        element.gameObject.layer = LayerMask.NameToLayer("Mouse Light");
        element.rb.bodyType = RigidbodyType2D.Dynamic;
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

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - element.rb.position;

        if(direction.magnitude > element.stoppingDistance)
        {
            direction.Normalize();
            Vector2 targetVelocity = direction * element.speed;

            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, element.acceleration * Time.fixedDeltaTime);

            Vector2 movement = currentVelocity * Time.fixedDeltaTime;

            element.rb.MovePosition(element.rb.position + movement);
        }
        else
        {
            element.rb.MovePosition(Vector2.MoveTowards(element.rb.position, mousePosition, element.speed * Time.fixedDeltaTime));

            currentVelocity = Vector2.zero;
            element.rb.velocity = Vector2.zero;
        }

        if (Input.GetButtonDown("Light Action"))
        {
            element.SwitchState(element.waitState);
        }
    }
}
