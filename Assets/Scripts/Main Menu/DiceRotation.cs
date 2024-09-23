using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    public float sensitivity = 0.1f; // Sensitivity factor for mouse movement
    public float baseRotationSpeed = 10f; // Speed of the continuous rotation
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        RotateWithMouse();
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate the rotation based on mouse movement (reversed)
        targetRotation *= Quaternion.Euler(mouseY * sensitivity, -mouseX * sensitivity, 0);

        // Smoothly interpolate the rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}