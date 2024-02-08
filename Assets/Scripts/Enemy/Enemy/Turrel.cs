using UnityEngine;

public class Turrel : EnemyManager
{
    private void Update()
    {
        if (!GameManager.instance.isGamePaused)
        {
            Movement();
            Rotation();
            AimAtPlayer();
            EnsureMaxHealth();
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
        if (Time.time > nextFire)
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
        foreach (var firePoint in firePoints)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
