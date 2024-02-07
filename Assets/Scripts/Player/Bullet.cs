using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;

    public GameObject explosion;

    PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Support"))
        {
            collision.GetComponent<EnemyManager>().TakeDamage(1);
            Destroy(gameObject);
        }

        if(collision.CompareTag("Terre"))
        {
            if (player.speed < 3)
            {
                collision.GetComponent<EnemyManager>().TakeDamage(1);
                Destroy(gameObject);
            }
            else
                return;
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
