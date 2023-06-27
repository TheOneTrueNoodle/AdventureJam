using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject[] colliders;

    public void enableColliders()
    {
        foreach(GameObject collider in colliders)
        {
            collider.SetActive(true);
        }
    }

    public void disableColliders()
    {
        foreach (GameObject collider in colliders)
        {
            collider.SetActive(false);
        }
    }
}
