using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Killzone : MonoBehaviour
{
    private const float TransitionTime = 6f;
    [SerializeField] private Image transition;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().canMove = false;

            StartCoroutine(respawn(collision.gameObject.GetComponent<PlayerMovement>()));
        }
    }

    private IEnumerator respawn(PlayerMovement player)
    {
        float alphaChange = 0;

        while (alphaChange < 1)
        {
            alphaChange += TransitionTime * Time.deltaTime;
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, alphaChange);
            yield return new WaitForSeconds(TransitionTime * Time.deltaTime);
        }

        player.canMove = true;
        player.transform.position = respawnPoint.position;

        while (alphaChange > 0)
        {
            alphaChange -= TransitionTime * Time.deltaTime;
            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, alphaChange);
            yield return new WaitForSeconds(TransitionTime * Time.deltaTime);
        }
    }
}
