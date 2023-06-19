using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalCutscene : MonoBehaviour
{
    public List<GameObject> Statues;
    public GameObject screenBlack;

    public void TriggerCutscene()
    {
        if(Statues != null)
        {
            StartCoroutine(ScreenFlash());
        }
    }

    IEnumerator ScreenFlash()
    {
        screenBlack.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        screenBlack.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        screenBlack.SetActive(true);

        for (int i = 0; i < Statues.Count; i++)
        {
            Statues[i].SetActive(false);
        }

        yield return new WaitForSeconds(0.2f);
        screenBlack.SetActive(false);
    }
}
