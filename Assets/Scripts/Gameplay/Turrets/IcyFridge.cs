using System.Collections;
using UnityEngine;

public class IcyFridge : MonoBehaviour
{
    public float waitTime;          // Cooldown time between shots
    public float raycastLength;     // Length of the raycast
    public float projectileForce;   // Force applied to the projectile
    private bool canShoot = true;

    public GameObject projectilePrefab;   // The projectile prefab
    public string targetTag = "Enemy";    // Tag of the target to shoot at

    private IEnumerator PerformActionCoroutine()
    {
        while(true){
        if (canShoot)
            {
                RaycastHit hit;
                // Cast a ray to detect objects in front of the turret
                bool raycastHit = Physics.Raycast(transform.position, transform.right, out hit, raycastLength);

                // Visualize the ray in the editor (green = no hit, red = hit)
                Debug.DrawRay(transform.position, transform.right * raycastLength, raycastHit ? Color.red : Color.green);

                // If a target is detected and the object has the correct tag
                if (raycastHit)
                {
                    // Fire at the target
                    Debug.Log("Target in sight: " + hit.collider.name);
                    PerformAction();
                    
                    // Enter cooldown period
                    canShoot = false;
                    yield return new WaitForSeconds(waitTime);  // Wait for the cooldown to finish
                    canShoot = true;  // Reset the flag so the turret can shoot again
                }
            }
            Debug.Log("on Cooldwon");

            yield return null;  // Continue checking each frame
        }
    }
        


    void Start()
    {
        // Start the coroutine when the game starts
        StartCoroutine(PerformActionCoroutine());
    }

    void PerformAction()
    {
        Debug.Log("Firing projectile!");

        // Instantiate the projectile at the current position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        // Apply a force to make the projectile move forward (using transform.right for direction)
        projectileRigidbody.AddForce(transform.right * projectileForce, ForceMode.Impulse);
    }
}
