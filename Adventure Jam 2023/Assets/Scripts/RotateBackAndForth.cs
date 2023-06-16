using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBackAndForth : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private float newRotation;
    [SerializeField] private float resetRotation;

    private Quaternion targetQuaternion;

    private void Start()
    {
        Reset();
    }

    public void Trigger()
    {
        targetQuaternion = Quaternion.Euler(new Vector3(0, 0, newRotation));
    }

    public void Reset()
    {
        targetQuaternion = Quaternion.Euler(new Vector3(0, 0, resetRotation));
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);
    }
}
