using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    private bool playerNear;
    private bool buttonActive;
    public UnityEvent buttonPressedAction;
    public UnityEvent buttonReleaseAction;

    [SerializeField] private GameObject ButtonObj;
    [SerializeField] private GameObject ButtonPressedObj;

    private void Update()
    {
        if(playerNear)
        {
            if(Input.GetButtonDown("Interact"))
            {
                triggerButtonState();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerNear = false;
        }
    }

    private void triggerButtonState()
    {
        if(buttonActive)
        {
            buttonActive = false;
            ButtonPressedObj.SetActive(false);
            ButtonObj.SetActive(true);
            buttonReleaseAction.Invoke();
        }
        else
        {
            buttonActive = true;
            ButtonPressedObj.SetActive(true);
            ButtonObj.SetActive(false);
            buttonPressedAction.Invoke();
        }
    }
}
