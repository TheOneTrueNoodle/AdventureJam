using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera areaCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            areaCam.Priority = 10;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            areaCam.Priority = 0;
        }
    }
}
