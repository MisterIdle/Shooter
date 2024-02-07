using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10;
    public float maxHealth = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Support"))
        {
            TakeDamage(3);
            Destroy(collision.gameObject);
        }
    }
}
