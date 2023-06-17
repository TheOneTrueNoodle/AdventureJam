using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMouseLight : MonoBehaviour
{
    public MouseLightController MouseLight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MouseLight.SwitchState(MouseLight.wakingState);
            collision.GetComponent<PlayerMovement>().canMove = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
