using UnityEngine;

public class BigExplosion : MonoBehaviour
{
    public float explosionDuration;
    public int explosionDamage;
    public float damageInterval;

    private float lastDamageTime;

    private void Start()
    {
        Destroy(gameObject, explosionDuration);
        lastDamageTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyDamage(other.GetComponent<PlayerHealth>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time - lastDamageTime > damageInterval)
        {
            ApplyDamage(collision.GetComponent<PlayerHealth>());
            lastDamageTime = Time.time;
        }
    }

    private void ApplyDamage(PlayerHealth playerHealth)
    {
        playerHealth.TakeDamage(explosionDamage);
    }
}
