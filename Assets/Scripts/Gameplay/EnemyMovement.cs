using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private int currentWaypointIndex = -1; // Start at -1 to correctly target w0 first
    private Transform targetWaypoint;
    private WaypointsManager waypointsManager;
    private bool reverse = false;

    void Start()
    {
        waypointsManager = FindObjectOfType<WaypointsManager>();
        SetInitialWaypoint();
    }

    void Update()
    {
        if (targetWaypoint != null)
        {
            MoveTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        Vector3 direction = targetWaypoint.position - transform.position;
        //Debug.Log($"Moving towards waypoint {currentWaypointIndex + 1}: {targetWaypoint.position}");
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            Debug.Log($"Reached waypoint {currentWaypointIndex + 1}");
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
                //Debug.Log($"Next waypoint: {currentWaypointIndex + 1} at {targetWaypoint.position}");
            }
            else
            {
                Debug.Log("No more waypoints.");
            }
        }
    }

    public void SetInitialWaypoint()
    {
        if (waypointsManager != null && waypointsManager.waypoints.Length > 0)
        {
            targetWaypoint = waypointsManager.waypoints[0]; // Set initial target to the spawn point
            Debug.Log($"Initial waypoint set to: {targetWaypoint.position}");
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
}