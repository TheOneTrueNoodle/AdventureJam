using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public bool singleAction;
    private float singleActionTimer;

    private bool playerNear;
    private bool buttonActive;
    public UnityEvent buttonPressedAction;
    public UnityEvent buttonReleaseAction;

    [SerializeField] private GameObject ButtonObj;
    [SerializeField] private GameObject ButtonPressedObj;

    private void Update()
    {
        if(singleActionTimer > 0)
        {
            singleActionTimer -= Time.deltaTime;
            if(singleActionTimer <= 0)
            {
                singleActionTimer = 0;
                buttonActive = false;
                ButtonPressedObj.SetActive(false);
                ButtonObj.SetActive(true);
            }
        }

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
        if (singleAction)
        {
            buttonActive = true;
            ButtonPressedObj.SetActive(true);
            ButtonObj.SetActive(false);
            buttonPressedAction.Invoke();

            singleActionTimer = 0.3f;

            return;
        }

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
