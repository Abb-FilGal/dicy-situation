using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // The target object to orbit around
    public float distance = 18.0f; // Initial distance from the target
    public float minDistance = 8.0f; // Minimum zoom distance
    public float maxDistance = 24.0f; // Maximum zoom distance
    public float zoomSpeed = 8.0f; // Speed of zooming
    public float rotationSpeed = 600.0f; // Speed of rotation
    public float deceleration = 5.0f; // Deceleration factor

    private float currentX = 0.0f; // Rotation around the X axis
    private float currentY = 0.0f; // Rotation around the Y axis
    private float xSmoothInput = 0.0f; // Smoothed input for X axis
    private float ySmoothInput = 0.0f; // Smoothed input for Y axis
    private bool isRotating = false; // Flag to check if the camera is rotating

    void Start()
    {
        // Initialize camera position
        if (target != null)
        {
            Vector3 angles = transform.eulerAngles;
            currentX = angles.y;
            currentY = angles.x;

            UpdateCameraPosition();
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Rotate the camera with the right mouse button
            if (Input.GetMouseButton(1))
            {
                xSmoothInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                ySmoothInput = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
                isRotating = true; // Set rotating flag to true
            }
            else
            {
                // Apply deceleration when RMB is released
                if (isRotating)
                {
                    xSmoothInput *= 1.0f - deceleration * Time.deltaTime;
                    ySmoothInput *= 1.0f - deceleration * Time.deltaTime;

                    if (Mathf.Abs(xSmoothInput) < 0.01f && Mathf.Abs(ySmoothInput) < 0.01f)
                    {
                        xSmoothInput = 0.0f;
                        ySmoothInput = 0.0f;
                        isRotating = false; // Stop rotating
                    }
                }
            }

            currentX += xSmoothInput;
            currentY += ySmoothInput;
            currentY = Mathf.Clamp(currentY, -89.9f, 89.9f); // Clamping the vertical rotation

            // Zoom the camera with the mouse scroll wheel
            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Snap camera to predefined angles based on key press
            if (Input.GetKeyDown(KeyCode.Alpha1)) { currentX = 0; currentY = 0; }    // Side 1
            if (Input.GetKeyDown(KeyCode.Alpha2)) { currentX = 0; currentY = -90; }  // Side 2
            if (Input.GetKeyDown(KeyCode.Alpha3)) { currentX = -90; currentY = 0; }  // Side 3
            if (Input.GetKeyDown(KeyCode.Alpha4)) { currentX = 90; currentY = 0; }   // Side 4
            if (Input.GetKeyDown(KeyCode.Alpha5)) { currentX = 0; currentY = 90; }   // Side 5
            if (Input.GetKeyDown(KeyCode.Alpha6)) { currentX = 180; currentY = 0; }  // Side 6

            UpdateCameraPosition();
        }
    }

    void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, 0, -distance);
        transform.position = rotation * direction + target.position;
        transform.LookAt(target);
    }
}