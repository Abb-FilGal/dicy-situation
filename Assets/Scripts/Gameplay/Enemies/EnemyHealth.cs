using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 100; // Default health points
    public delegate void DeathHandler();
    public event DeathHandler OnDeath;
    public int threatLevel; // Default threat level
    public bool isBurning = false; // Default burn status

    private float burnInterval = 1.0f; // Interval for burn damage
    private int burnDamage = 5; // Damage per burn tick
    private Coroutine burnCoroutine;

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

    // Apply burn status
    public void ApplyBurn()
    {
        if (!isBurning)
        {
            isBurning = true;
            burnCoroutine = StartCoroutine(BurnDamageCoroutine());
        }
    }

    // Coroutine for applying burn damage
    private IEnumerator BurnDamageCoroutine()
    {
        while (isBurning)
        {
            TakeDamage(burnDamage);
            yield return new WaitForSeconds(burnInterval);
        }
    }

    // Remove burn status
    public void RemoveBurn()
    {
        if (isBurning)
        {
            isBurning = false;
            if (burnCoroutine != null)
            {
                StopCoroutine(burnCoroutine);
                burnCoroutine = null;
            }
        }
    }
}