using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyFridge : MonoBehaviour
{
    // Update is called once per frame
    public float waitTime;
    public float raycastLength;
    public float projectileForce;

    public GameObject projectilePrefab;

    IEnumerator PerformActionCoroutine()
    {
        while (true)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(waitTime);
            yield return waitForSeconds;
            Debug.Log("TESINGT");
  
            // Cast a ray forward
            RaycastHit hit;
            bool raycastHit = Physics.Raycast(transform.position, transform.right, out hit, raycastLength);
            
            // Draw a visible ray in the scene view
            Debug.DrawRay(transform.position, transform.right * raycastLength, raycastHit ? Color.red : Color.green);

            if (raycastHit)
            {
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

        // Instantiate the projectile prefab
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Get the rigidbody component of the projectile
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        // Apply a force to make the projectile move forward
        projectileRigidbody.AddForce(transform.right * projectileForce, ForceMode.Impulse);
    }
}
