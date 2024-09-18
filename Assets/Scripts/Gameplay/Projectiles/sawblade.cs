using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawblade : MonoBehaviour
{
    [Header("Projectile Stats")]
    public int Pierce;
    public int Damage;
    public float Lifetime;

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log( GetType().Name + " hit SAW");
            other.transform.parent.GetComponent<EnemyHealth>().TakeDamage(Damage, "sawblade");
            Pierce--;
            if (Pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

