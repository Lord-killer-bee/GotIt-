using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private Vector3 initialRotation;

    private bool startWatch = false;

    public void WatchRocket()
    {
        startWatch = true;
    }

    private void Update()
    {
        if (startWatch)
        {
            transform.LookAt(rocket.transform);
        }
    }

    public void ResetCamera()
    {
        transform.localEulerAngles = initialRotation;
        startWatch = false;
    }
}
