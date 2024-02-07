using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("EnemyType")]
    public bool canShoot;
    public bool isProtected;
    public bool isSpeed;

    [Header("Movement")]
    public float speed;
    public float rotation;
    public SpriteRenderer sprite;
    public int minDistanceWithEnemy;
    public int maxDistanceWithPlayer;

    [Header("Health")]
    public float health;
    private float maxHealth;

    [Header("Shooting")]
    [ShowIf("canShoot")]
    public GameObject bullet;
    [ShowIf("canShoot")]
    public List<Transform> firePoints;
    [ShowIf("canShoot")]
    public bool firePointsLookAtPlayer;
    [ShowIf("canShoot")]
    public float fireRate;
    [ShowIf("canShoot")]
    public float numFire;
    [ShowIf("canShoot")]
    public float timeBetweenShots;
    [ShowIf("canShoot")]
    public int minDistanceForShooting;
    [ShowIf("canShoot")]
    public float nextFire;

    private PlayerMovement player;
    private PlayerHealth playerHealth;

    public PlayerMovement PlayerMovement
    {
        get => player;
        set => player = value;
    }

    public PlayerHealth PlayerHealth
    {
        get => playerHealth;
        set => playerHealth = value;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        maxHealth = health;
    }

    protected void Movement()
    {
        if (isSpeed)
        {
            Vector3 movement = sprite.transform.up;
            transform.position += movement * Mathf.Lerp(speed, speed * 2, 3) * Time.deltaTime;
        }
        else
        {
            Vector3 movement = sprite.transform.up;
            transform.position += movement * speed * Time.deltaTime;
        }
    }

    protected void Rotation()
    {
        if (player == null) return;

        Vector3 playerPos = player.transform.position;
        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.LookRotation(Vector3.forward, playerPos - sprite.transform.position), rotation * Time.deltaTime);
    }

    protected void KeepDistanceWithEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        HashSet<GameObject> processedEnemies = new HashSet<GameObject>();

        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == gameObject || processedEnemies.Contains(enemy))
                continue;

            processedEnemies.Add(enemy);

            Vector3 enemyPosition = enemy.transform.position;
            float sqrDistance = (currentPosition - enemyPosition).sqrMagnitude;

            if (sqrDistance < minDistanceWithEnemy * minDistanceWithEnemy)
            {
                Vector3 directionToEnemy = currentPosition - enemyPosition;
                sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.LookRotation(Vector3.forward, directionToEnemy), rotation * Time.deltaTime);
            }
        }
    }

    protected void KeepDistanceWithPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < maxDistanceWithPlayer)
        {
            Vector3 directionToPlayer = transform.position - player.transform.position;
            sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.LookRotation(Vector3.forward, directionToPlayer), rotation * Time.deltaTime);
        }
        else
        {
            Rotation();
        }
    }

    protected void ForceRotate()
    {
        if(player.transform.position.x > transform.position.x)
        {
            sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector3.left), rotation * Time.deltaTime);
        }
        else
        {
            sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector3.right), rotation * Time.deltaTime);
        } 

    }

    public void EnsureMaxHealth()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
            Debug.Log("Health capped at max health: " + maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (!isProtected)
        {
            health -= damage;
            Debug.Log(health + " Non protect");
        }
        else
        {
            health -= damage - 1;
            Debug.Log(health + " protect");
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Bombarder bombarder = FindObjectOfType<Bombarder>();
        if(bombarder != null)
        {
            bombarder.Explose();
        }

        Destroy(gameObject);
    }
}
