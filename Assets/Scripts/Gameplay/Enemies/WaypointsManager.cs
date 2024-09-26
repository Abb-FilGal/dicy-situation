using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform GetNextWaypoint(int currentWaypointIndex, bool reverse = false)
    {
        if (waypoints != null && currentWaypointIndex >= 0 && currentWaypointIndex < waypoints.Length)
        {
            if (reverse)
            {
                if (currentWaypointIndex > 0)
                {
                    return waypoints[currentWaypointIndex - 1];
                }
            }
            else
            {
                if (currentWaypointIndex < waypoints.Length - 1)
                {
                    return waypoints[currentWaypointIndex + 1];
                }
            }
        }
        return null; // No more waypoints or invalid index
    }
}