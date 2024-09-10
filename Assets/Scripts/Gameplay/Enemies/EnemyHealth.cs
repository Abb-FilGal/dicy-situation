using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 100; // Default health points
    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    // Method to take damage
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    // Method to handle enemy death
    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}