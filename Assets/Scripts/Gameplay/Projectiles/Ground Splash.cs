using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSplash : MonoBehaviour
{
    [Header("Projectile Stats")]
    public int Pierce;
    public int Damage;
    public float Lifetime;
    private Vector3 initialPosition;   // Starting position of the object
    private Vector3 initialScale;      // Initial scale of the object 

    void Start()
    {
        Destroy(gameObject, Lifetime);
        // initialPosition = transform.position;
        // initialScale = transform.localScale;
    }

    void Update()
    {
        // float distanceTraveled = Vector3.Distance(initialPosition, transform.position);

        // // Calculate the new size by growing along the object's forward direction
        // Vector3 scaleIncrease = transform.right.normalized * distanceTraveled;

        // // Apply the growth uniformly in all directions relative to its initial scale
        // transform.localScale = new Vector3(
        //     initialScale.x + Mathf.Abs(scaleIncrease.x),
        //     initialScale.y + Mathf.Abs(scaleIncrease.y),
        //     initialScale.z + Mathf.Abs(scaleIncrease.z)
        // );

        // // Adjust the position to keep the back of the object fixed at the initial position
        // transform.position = initialPosition + transform.right * (transform.localScale.magnitude / 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit OIL");
            other.transform.parent.GetComponent<EnemyHealth>().TakeDamage(Damage, "OilSplash");
            other.transform.parent.GetComponent<EnemyMovement>().ApplyOil();
            Pierce--;
            if (Pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
