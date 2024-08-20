using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // The target object to orbit around
    public float distance = 5.0f; // Initial distance from the target
    public float minDistance = 8.0f; // Minimum zoom distance
    public float maxDistance = 18.0f; // Maximum zoom distance
    public float zoomSpeed = 8.0f; // Speed of zooming
    public float rotationSpeed = 600.0f; // Speed of rotation

    private float currentX = 0.0f; // Rotation around the X axis
    private float currentY = 0.0f; // Rotation around the Y axis

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
                currentX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                currentY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
                currentY = Mathf.Clamp(currentY, -89.9f, 89.9f); // Clamping the vertical rotation
            }

            // Zoom the camera with the mouse scroll wheel
            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Snap camera to predefined angles based on key press
            if (Input.GetKeyDown(KeyCode.Alpha1)) { currentX = 0; currentY = 0; }    // Front
            if (Input.GetKeyDown(KeyCode.Alpha2)) { currentX = 180; currentY = 0; }  // Back
            if (Input.GetKeyDown(KeyCode.Alpha3)) { currentX = -90; currentY = 0; }  // Left
            if (Input.GetKeyDown(KeyCode.Alpha4)) { currentX = 90; currentY = 0; }   // Right
            if (Input.GetKeyDown(KeyCode.Alpha5)) { currentX = 0; currentY = 90; }   // Top
            if (Input.GetKeyDown(KeyCode.Alpha6)) { currentX = 0; currentY = -90; }  // Bottom

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
