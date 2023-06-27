using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMirrors : MonoBehaviour
{
    public Color enabledColor;
    public Color disabledColor;
    public List<Mirror> mirrors;

    private void Start()
    {
        foreach(Mirror mirror in mirrors)
        {
            mirror.spriteRenderer.color = disabledColor;
            mirror.disableColliders();
        }
    }

    public void enable()
    {
        foreach (Mirror mirror in mirrors)
        {
            mirror.spriteRenderer.color = enabledColor;
            mirror.disableColliders();
        }
    }
}
