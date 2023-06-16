using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        anim.Play("Door opening");
    }

    public void Close()
    {
        anim.Play("Door Closing");
    }
}
