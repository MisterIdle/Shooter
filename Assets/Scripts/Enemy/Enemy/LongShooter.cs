using UnityEngine;

public class LongShooter : EnemyManager
{
    private void Update()
    {
        Movement();
        Rotation();
        AimAtPlayer();
        KeepDistanceWithEnemy();
        KeepDistanceWithPlayer();
        EnsureMaxHealth();

        if (PlayerMovement == null) return;
        if (Vector3.Distance(PlayerMovement.transform.position, transform.position) <= minDistanceForShooting)
        {
            SpawnBullet();
        }
    }

    public void AimAtPlayer()
    {
        if (firePointsLookAtPlayer && PlayerMovement != null)
        {
            foreach (Transform firePoint in firePoints)
            {
                Vector2 direction = (Vector2)PlayerMovement.transform.position - (Vector2)firePoint.position;
                firePoint.up = direction;
            }
        }
    }

    private void SpawnBullet()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            for (int i = 0; i < numFire; i++)
            {
                Invoke("FireBullet", i * timeBetweenShots);
            }
        }
    }

    private void FireBullet()
    {
        Instantiate(bullet, firePoints[0].position, firePoints[0].rotation);
    }
}
