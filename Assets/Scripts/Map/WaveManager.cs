using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
    public int groupCount;
    public float spawnRate;
    public float timeSinceLastSpawn;
    public bool sameSpawnPoint;
}

[System.Serializable]
public class WaveData
{
    public List<EnemyType> enemyTypes;
    public float waveDuration;

    public WaveData()
    {
        enemyTypes = new List<EnemyType>();
        waveDuration = 0f;
    }
}

public class WaveManager : MonoBehaviour
{
    public PolygonCollider2D zone;
    public HUDManager hudManager;
    public PlayerMovement playerMovement;
    public List<EnemyType> enemyTypes;

    public float publicTimer;

    public int waveNumber;
    public List<WaveData> wavesData;
    public bool isWaveActive;
    private float waveStartTime;

    public void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        hudManager = FindObjectOfType<HUDManager>();
        zone = GetComponent<PolygonCollider2D>();
        StartNextWave();
    }


    public void Update()
    {
        if (isWaveActive && waveNumber < wavesData.Count && wavesData[waveNumber].enemyTypes.Count > 0)
        {
            publicTimer = wavesData[waveNumber].waveDuration - (Time.time - waveStartTime);
            foreach (EnemyType enemyType in wavesData[waveNumber].enemyTypes)
            {
                if (Time.time > enemyType.timeSinceLastSpawn + enemyType.spawnRate)
                {
                    SpawnEnemy(enemyType);
                    enemyType.timeSinceLastSpawn = Time.time;
                }
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                StopWave();
                Debug.Log("Wave stopped");
            }
            else if (Time.time - waveStartTime > wavesData[waveNumber].waveDuration)
            {
                StopWave();
            }
        }
        else
        {
            hudManager.menu.SetActive(true);
            isWaveActive = false;
            DestroyAllEnemies();
        }

        ChangeWaveWithArrows();
    }

    public void SpawnEnemy(EnemyType enemyType)
    {
        Vector2 spawnPosition;
        if (enemyType.sameSpawnPoint)
        {
            spawnPosition = RandomPointOutsideZone();
            for (int i = 0; i < enemyType.groupCount; i++)
            {
                Instantiate(enemyType.enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            for (int i = 0; i < enemyType.groupCount; i++)
            {
                spawnPosition = RandomPointOutsideZone();
                Instantiate(enemyType.enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector2 RandomPointOutsideZone()
    {
        Vector2 randomPoint;
        do
        {
            randomPoint = new Vector2(Random.Range(zone.bounds.min.x - 5f, zone.bounds.max.x + 5f),
                                      Random.Range(zone.bounds.min.y - 5f, zone.bounds.max.y + 5f));
        } while (zone.OverlapPoint(randomPoint));

        return randomPoint;
    }

    private void DestroyAllEnemies()
    {
        foreach (EnemyManager enemy in FindObjectsOfType<EnemyManager>())
        {
            enemy.TakeDamage(100);
        }
    }

    public void StopWave()
    {
        isWaveActive = false;
        DestroyAllEnemies();

        playerMovement.spriteRenderer.enabled = false;
        GameManager.instance.isGameMenu = true;
    }

    public void NextWaveButtonClicked()
    {
        if (!isWaveActive && waveNumber < wavesData.Count)
        {
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        isWaveActive = true;
        waveStartTime = Time.time;
        waveNumber++;

        hudManager.menu.SetActive(false);
        playerMovement.spriteRenderer.enabled = true;
        GameManager.instance.isGameMenu = false;
    }

    // DEBUG

    private void ChangeWaveWithArrows()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isWaveActive && waveNumber < wavesData.Count - 1)
        {
            waveNumber++;
            Debug.Log("Switched to wave " + waveNumber);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isWaveActive && waveNumber > 0)
        {
            waveNumber--;
            Debug.Log("Switched to wave " + waveNumber);
        }
    }
}
