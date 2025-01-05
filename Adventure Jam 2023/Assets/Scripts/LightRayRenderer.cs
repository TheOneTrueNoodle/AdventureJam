using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRayRenderer : MonoBehaviour
{
    [Header("Light Prism GFX")]
    public bool hasLight;
    public float lightChargeRate = 10f;

    [Header("Debug Display")]
    public float lightCurrentCharge;
    public bool charged;

    [Header("Light Ray Stats")]
    public direction shootDirection;
    public float lightRayCooldown = 1f;
    private float lightRayCooldownCurrent;
    public GameObject lightRayPrefab;

    private SpriteRenderer gfx;

    private void Start()
    {
        lightRayCooldownCurrent = lightRayCooldown;
        lightCurrentCharge = 0;
        gfx = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(hasLight)
        {
            if(charged)
            {
                if (lightRayCooldownCurrent <= 0)
                {
                    //Shoot ray
                    lightRayCooldownCurrent = lightRayCooldown;
                    GameObject newRay = Instantiate(lightRayPrefab);
                    newRay.transform.position = transform.position;

                    LightRay lightRay = newRay.GetComponent<LightRay>();

                    lightRay.CreateRay(this, getDirection(shootDirection));
                }
                else
                {
                    lightRayCooldownCurrent -= Time.deltaTime;
                }
            }
            else
            {
                lightCurrentCharge += lightChargeRate * Time.deltaTime;
                if(lightCurrentCharge >= 1f)
                {
                    lightCurrentCharge = 1f;
                    charged = true;
                }
            }
        }
        else
        {
            charged = false;
            if (lightCurrentCharge > 0)
            {
                lightCurrentCharge -= lightChargeRate * Time.deltaTime;
                if(lightCurrentCharge < 0) { lightCurrentCharge = 0; }
            }
        }

        gfx.color = new Color(255, 255, 255, lightCurrentCharge);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Mouse Light")
        {
            hasLight = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mouse Light")
        {
            hasLight = false;
        }
    }

    private Vector2 getDirection(direction dir)
    {
        if(dir == direction.Up)
        {
            return Vector2.up;
        }
        if (dir == direction.Down)
        {
            return Vector2.down;
        }
        if (dir == direction.Left)
        {
            return Vector2.left;
        }
        if (dir == direction.Right)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.right;
        }
    }
}

public enum direction
{
    Up,
    Down,
    Left,
    Right
}
