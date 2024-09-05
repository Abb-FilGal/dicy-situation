using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeFrog : MonoBehaviour
{
    public int counter = 0;

    // Update is called once per frame
    public float waitTime = 1.2f;

    IEnumerator PerformActionCoroutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(waitTime);

        while (true)
        {
            yield return waitForSeconds;

            // Cast a ray forward
            RaycastHit hit;
            float raycastLength = 2.5f; // Change the length of the raycast here
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
