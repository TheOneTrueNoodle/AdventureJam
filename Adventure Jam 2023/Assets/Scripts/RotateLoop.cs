using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    private float rotation;

    private Quaternion targetQuaternion;

    private void Start()
    {
        rotation = transform.rotation.z;
    }

    public void Trigger()
    {
        rotation += 90;
        targetQuaternion = Quaternion.Euler(new Vector3(0, 0, rotation));
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);
    }
}
