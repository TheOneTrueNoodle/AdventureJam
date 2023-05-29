using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRayRenderer : MonoBehaviour
{
    public bool hasLight;

    [Header("Light Ray Stats")]
    public direction shootDirection;
    public float lightRayCooldown = 1f;
    private float lightRayCooldownCurrent;
    public GameObject lightRayPrefab;
    [HideInInspector] public List<GameObject> activeRays = new List<GameObject>();

    private void Start()
    {
        lightRayCooldownCurrent = lightRayCooldown;
    }

    private void Update()
    {
        if(hasLight)
        {
            if(lightRayCooldownCurrent <= 0)
            {
                //Shoot ray
                lightRayCooldownCurrent = lightRayCooldown;
                GameObject newRay = Instantiate(lightRayPrefab);
                newRay.transform.position = transform.position;

                LightRay lightRay = newRay.GetComponent<LightRay>();

                int rayID = activeRays.Count;
                activeRays.Add(newRay);

                lightRay.CreateRay(this, getDirection(shootDirection), rayID);
            }
            else
            {
                lightRayCooldownCurrent -= Time.deltaTime;
            }
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
