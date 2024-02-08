using UnityEngine;

public class Krips : EnemyManager
{
    private void Update()
    {
        if (!GameManager.instance.isGamePaused)
        {
            Movement();
            Rotation();
            KeepDistanceWithEnemy();
            EnsureMaxHealth();

            if (canShoot)
            {
                KeepDistanceWithPlayer();
                SpawnKrips();
            }
        }
    }

    public void SpawnKrips()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireKrips();
        }
    }

    private void FireKrips()
    {
        foreach (var firePoint in firePoints)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
