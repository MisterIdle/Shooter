using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletRotation;

    public SpriteRenderer sprite;
    public GameObject bigExplosion;

    private Bombarder bombarder;
    private PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        bombarder = FindObjectOfType<Bombarder>();
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
            if (bombarder.followingPlayer)
            {
                Movement();
                Rotation();
            }
            else
            {
                Movement();
            }
        }
    }


    private void Movement()
    {
        Vector3 movement = sprite.transform.up;
        transform.position += movement * bulletSpeed * Time.deltaTime;
    }

    private void Rotation()
    {
        Vector3 playerPos = player.transform.position;
        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.LookRotation(Vector3.forward, playerPos - sprite.transform.position), bulletRotation * Time.deltaTime);
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
        if (bombarder.explose)
        {
            Instantiate(bigExplosion, transform.position, Quaternion.identity);
            Debug.Log("Explosion");
        }
    }

}
