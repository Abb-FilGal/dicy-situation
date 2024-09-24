using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int currentWaypointIndex = -1; // Start at -1 to correctly target w0 first
    private Transform targetWaypoint;
    private WaypointsManager waypointsManager;
    private bool reverse = false;
    public float baseSpeed = 1.0f; // Speed of the enemy
    private float currentSpeed; // Speed of the enemy after status adjustments
    public bool isOiledUp = false; // Default oil status
    public bool isFrozen = false; // Default freeze status
    public int freezeDuration = 3; // Default freeze duration

    void Start()
    {
        waypointsManager = FindObjectOfType<WaypointsManager>();
        SetInitialWaypoint();
    }

    // Apply oil status
    public void ApplyOil()
    {
        isOiledUp = true;
    }

    // Remove oil status
    public void RemoveOil()
    {
        isOiledUp = false;
    }

    // Apply freeze status
    public void ApplyFreeze()
    {
        isFrozen = true;
        Invoke("RemoveFreeze", freezeDuration);
    }

    // Remove freeze status
    public void RemoveFreeze()
    {
        isFrozen = false;
    }

    void Update()
    {
        AdjustSpeed();

        if (targetWaypoint != null)
        {
            MoveTowardsWaypoint();
        }
    }

    // Adjust baseSpeed based on status
    private void AdjustSpeed()
    {
        const float speedMultiplier = 0.375f;
        currentSpeed = baseSpeed * speedMultiplier;

        //Debug.Log($"Base speed: {baseSpeed}, Speed multiplier: {speedMultiplier}, Current speed after multiplier: {currentSpeed}");

        if (isOiledUp)
        {
            currentSpeed *= 0.8f;
            Debug.Log($"Oiled up: {isOiledUp}, Current speed after oil adjustment: {currentSpeed}");
        }

        if (isFrozen)
        {
            currentSpeed *= 0.5f;
            Debug.Log($"Frozen: {isFrozen}, Current speed after freeze adjustment: {currentSpeed}");
        }

        if (isOiledUp && isFrozen)
        {
            currentSpeed *= 0.4f;
            Debug.Log($"Oiled up and frozen: {isOiledUp && isFrozen}, Current speed after both adjustments: {currentSpeed}");
        }
    }

    void MoveTowardsWaypoint()
    {
        Vector3 direction = targetWaypoint.position - transform.position;
        //Debug.Log($"Moving towards waypoint {currentWaypointIndex + 1}: {targetWaypoint.position}");
        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            //Debug.Log($"Reached waypoint {currentWaypointIndex + 1}");
            if (reverse)
            {
                currentWaypointIndex--;
            }
            else
            {
                currentWaypointIndex++;
            }
            targetWaypoint = waypointsManager.GetNextWaypoint(currentWaypointIndex, reverse);
            if (targetWaypoint != null)
            {
                // Update rotation to match the waypoint's rotation
                UpdateRotation();
                //Debug.Log($"Next waypoint: {currentWaypointIndex + 1} at {targetWaypoint.position}");
            }
            else
            {
                // Handle case when there are no more waypoints
            }
        }
    }

    public void SetInitialWaypoint()
    {
        if (waypointsManager != null && waypointsManager.waypoints.Length > 0)
        {
            targetWaypoint = waypointsManager.waypoints[0]; // Set initial target to the spawn point
            UpdateRotation();
            //Debug.Log($"Initial waypoint set to: {targetWaypoint.position}");
        }
    }

    public void SetReverse(bool isReverse)
    {
        reverse = isReverse;
        Debug.Log($"Reverse set to: {reverse}");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBase"))
        {
            // Handle game over logic
            Debug.Log("Game Over!");
        }
    }

    private void UpdateRotation()
    {
        if (targetWaypoint != null)
        {
            transform.rotation = targetWaypoint.rotation;
        }
    }
}