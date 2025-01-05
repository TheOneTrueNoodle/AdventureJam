using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightRay : MonoBehaviour
{
    public LightRayRenderer parentRenderer;
    public Vector3 currentDirection;

    public float raySpeed = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void CreateRay(LightRayRenderer renderer, Vector2 direction)
    {
        parentRenderer = renderer;
        currentDirection = direction;
    }

    private void FixedUpdate()
    {
        rb.velocity = currentDirection * raySpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Mirror"))
        {
            //Reflect this object
            Vector2 newDirection = Vector2.Reflect(currentDirection, collision.contacts[0].normal);
            currentDirection = newDirection;
        }
        else if (collision.gameObject.GetComponent<LightRayGoal>())
        {
            collision.gameObject.GetComponent<LightRayGoal>().Activate();
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Platform") != true)
        {
            Destroy(gameObject);
        }
    }
}
