using UnityEngine;

public class RocketTank : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;

    public SpriteRenderer sprite;
    public GameObject bigExplosion;

    private void Start()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        bulletLifeTime -= Time.deltaTime;

        if (bulletLifeTime <= 0.1)
        {
            Explosion();
            Destroy(gameObject);
        }
        else
        {
            Movement();
        }
    }


    private void Movement()
    {
        Vector3 movement = sprite.transform.up;
        transform.position += movement * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(1);
            Explosion();
            Destroy(gameObject);
        }
    }

    private void Explosion()
    {
        Instantiate(bigExplosion, transform.position, Quaternion.identity);
    }

}
