using System.Collections;
using UnityEngine;

public class OilBlaster : MonoBehaviour
{
    public float waitTime;          // Cooldown time between shots
    public float raycastLength;     // Length of the raycast
    public float projectileForce;   // Force applied to the projectile
    public bool canShoot = true;
    public float distance = 0.5f;
    private Vector3 groundPosition;
    public Vector3 platformScale = new Vector3(2f, 1f, 1f);  // Scale of the platform

    public GameObject projectilePrefab;   // The projectile prefab
    public GameObject groundOilPrefab;
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
                if (raycastHit && hit.collider.CompareTag("Enemy"))
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
        // groundPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    void PerformAction()
    {
        Debug.Log("Firing OIL!");

        // Instantiate the projectile at the current position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        // GameObject groundOil = Instantiate(groundOilPrefab, transform.position, transform.rotation);
        SpawnPlatformToRight();
        // Get the Rigidbody component of the projectile
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        
        // Rigidbody groundOilRigidbody = groundOil.GetComponent<Rigidbody>();

        // Apply a force to make the projectile move forward (using transform.right for direction)
        projectileRigidbody.AddForce(transform.right * projectileForce, ForceMode.Impulse);
        // groundOilRigidbody.AddForce(transform.right * projectileForce, ForceMode.Impulse);

    }
    
    void SpawnPlatformToRight()
    {
        // Get the right direction of the object, considering its current rotation
        Vector3 rightDirection = transform.right;

        // Calculate the position for the platform to spawn (to the right of the object)
        Vector3 spawnPosition = transform.position + transform.right;

        // Instantiate the platform at the calculated position with right rotation
        GameObject newPlatform = Instantiate(groundOilPrefab, spawnPosition, transform.rotation);

        // Optionally set the platform's scale
        Debug.Log("Setting new Scale");
        // newPlatform.transform.localScale = new Vector3(2f, 1f, 1f);
        Debug.Log("Set new Scale");
        // Optionally set the platform's rotation to match the current object's rotation
        newPlatform.transform.rotation = transform.rotation;

        Debug.Log("Platform spawned to the right!");
    }
}
