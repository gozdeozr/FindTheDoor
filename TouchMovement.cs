using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform player;

    [SerializeField] Joystick lookJoystick;

    float xRotation = 0f;
    float yRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        float X = lookJoystick.Horizontal * sensitivity * Time.deltaTime;
        float Y = lookJoystick.Vertical * sensitivity * Time.deltaTime;

        xRotation -= Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += X;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        player.Rotate(Vector3.up * X);
    }
}
