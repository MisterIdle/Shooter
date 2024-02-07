using System.Linq;
using UnityEngine;

public class Support : EnemyManager
{
    [Header("Support")]
    public bool isHealing;
    public bool isProtecting;
    public bool isBoost;

    public float healDistance;
    public float protectDistance;
    public float boostDistance;
    public float healInterval = 3f;
    private float lastHealTime;

    public GameObject healthEffect;
    public GameObject protectEffect;
    public GameObject boostEffect;

    GameObject[] enemies;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Where(enemy => enemy != gameObject).ToArray();
    }

    public void Update()
    {
        Movement();
        RotateInOtherEnemy();

        if (isHealing && Time.time - lastHealTime >= healInterval)
        {
            HealOtherEnemy();
            lastHealTime = Time.time;
        }

        if (isProtecting)
        {
            ProtectOtherEnemy();
        }

        if (isBoost)
        {
            BoostOtherEnemy();
        }
    }

    private void RotateInOtherEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null && sprite != null) // Check if enemy and sprite are not null
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) < Mathf.Infinity)
                {
                    Vector2 direction = enemy.transform.position - transform.position;
                    sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.LookRotation(Vector3.forward, direction), rotation * Time.deltaTime);
                }
            }
        }
    }


    public void HealOtherEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < healDistance)
                {
                    EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
                    if (enemyManager != null)
                    {
                        enemyManager.health++;

                        GameObject healthEffectInstance = Instantiate(healthEffect, enemy.transform.position, Quaternion.identity);
                        healthEffectInstance.transform.SetParent(enemy.transform);

                        Destroy(healthEffectInstance, 1f);
                    }
                }
            }
        }
    }

    public void BoostOtherEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) // Check if the enemy GameObject is not null
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < boostDistance)
                {
                    EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
                    if (enemyManager != null)
                    {
                        if (!enemyManager.isSpeed)
                        {
                            GameObject speedEffectInstance = Instantiate(boostEffect, enemy.transform.position, Quaternion.identity);
                            speedEffectInstance.transform.SetParent(enemy.transform);
                            enemyManager.isSpeed = true;
                        }
                    }
                }
                else
                {
                    EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
                    if (enemyManager != null && enemyManager.isSpeed)
                    {
                        enemyManager.isSpeed = false;

                        // Check if the enemy still exists before accessing its child objects
                        if (enemy != null && enemy.transform != null)
                        {
                            foreach (Transform child in enemy.transform)
                            {
                                if (child.name == "BoostEffect(Clone)")
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    public void ProtectOtherEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) // Check if the enemy GameObject is not null
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < protectDistance)
                {
                    EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
                    if (enemyManager != null)
                    {
                        if (!enemyManager.isProtected)
                        {
                            GameObject protectEffectInstance = Instantiate(protectEffect, enemy.transform.position, Quaternion.identity);
                            protectEffectInstance.transform.SetParent(enemy.transform);
                            enemyManager.isProtected = true;
                        }
                    }
                }
                else
                {
                    EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
                    if (enemyManager != null && enemyManager.isProtected)
                    {
                        enemyManager.isProtected = false;

                        // Check if the enemy still exists before accessing its child objects
                        if (enemy != null && enemy.transform != null)
                        {
                            foreach (Transform child in enemy.transform)
                            {
                                if (child.name == "ProtectEffect(Clone)")
                                {
                                    Destroy(child.gameObject);
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    private void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
                if (enemyManager != null)
                {
                    if (enemyManager.isProtected)
                    {
                        enemyManager.isProtected = false;

                        foreach (Transform child in enemy.transform)
                        {
                            if (child.name == "ProtectEffect(Clone)")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                    }

                    if (enemyManager.isSpeed)
                    {
                        enemyManager.isSpeed = false;

                        foreach (Transform child in enemy.transform)
                        {
                            if (child.name == "BoostEffect(Clone)")
                            {
                                Destroy(child.gameObject);
                            }
                        }
                    }
                }
            }
        }   
    }
}
