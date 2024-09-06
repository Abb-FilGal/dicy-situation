using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTurret : MonoBehaviour
{
    public int counter = 0;

    // Update is called once per frame
    public float waitTime;
    public float raycastLength;

    IEnumerator PerformActionCoroutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(waitTime);

        while (true)
        {
            yield return waitForSeconds;

            // Cast a ray forward
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.right, Color.green);
            if (Physics.Raycast(transform.position, transform.right, out hit, raycastLength))
            {
                // Draw a visible ray in the scene view
                Debug.DrawRay(transform.position, transform.right * hit.distance, Color.red);

                // Check if the ray hits an object with the "Enemy" tag
                if (hit.collider.CompareTag("Enemy"))
                {
                    PerformAction();
                }
            }
        }
    }

    void Start()
    {
        StartCoroutine(PerformActionCoroutine());
    }

    void PerformAction()
    {
        Debug.Log("Performing action!");
    }
}
