using UnityEngine;

public class Bombarder : EnemyManager
{
    [Header("Bombarder")]
    public bool followingPlayer;
    public bool explose;
    public bool selfExplose;
    public bool isFleeing;
    public float fleeSpeed;
    public float dashSpeed;
    public float fleeRotationAmount;
    public float distanceWithPlayerMin;
    public float distanceWithPlayerMax;

    public GameObject bigExplosion;

    private float lastFireTime;

    private void Update()
    {
        if (!GameManager.instance.isGamePaused)
        {
            if (isFleeing)
            {
                Flee(PlayerMovement.transform.position);
                ForceRotate();
            }
            else
            {
                Dash();
            }

            Movement();
            Rotation();
            EnsureMaxHealth();

            float distanceToPlayer = Vector2.Distance(transform.position, PlayerMovement.transform.position);

            if (isFleeing)
            {
                if (distanceToPlayer >= distanceWithPlayerMax)
                {
                    isFleeing = false;
                }
            }
            else
            {
                if (distanceToPlayer <= distanceWithPlayerMin)
                {
                    isFleeing = true;

                    if (Time.time > lastFireTime + fireRate)
                    {
                        lastFireTime = Time.time;
                        SpawnBullet();
                    }
                }
            }
        }
    }

    private void SpawnBullet()
    {
        foreach (var firePoint in firePoints)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }

    public void Flee(Vector3 playerPosition)
    {
        Vector3 fleeDirection = (transform.position - playerPosition).normalized;
        Quaternion fleeRotation = Quaternion.LookRotation(Vector3.forward, fleeDirection) * Quaternion.Euler(0, 0, fleeRotationAmount);
        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, fleeRotation, Time.deltaTime);

        speed = fleeSpeed;
    }

    public void Dash()
    {
        speed = dashSpeed;
    }

    public void Explose()
    {
        if(selfExplose)
        {
            Instantiate(bigExplosion, transform.position, Quaternion.identity);
        }
    }
}
