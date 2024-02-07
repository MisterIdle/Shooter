using UnityEngine;
using System.Collections.Generic;

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
public struct WaveData
{
    public List<EnemyType> enemyTypes;
    public float waveDuration;

    public WaveData(List<EnemyType> types, float duration)
    {
        enemyTypes = types;
        waveDuration = duration;
    }
}

public class GameManager : MonoBehaviour
{
    public PolygonCollider2D zone;
    public List<EnemyType> enemyTypes;

    public int waveNumber;
    public List<WaveData> wavesData;
    public bool isWaveActive;
    private float waveStartTime;

    public void Start()
    {
        zone = GetComponent<PolygonCollider2D>();
    }

    public void Update()
    {
        StartWave();

        if (isWaveActive && waveNumber < wavesData.Count && wavesData[waveNumber].enemyTypes.Count > 0)
        {
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
            isWaveActive = false;
            DestroyAllEnemies();
        }

        ChangeWaveWithArrows(); // Appel de la nouvelle méthode
    }

    public void StartWave()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isWaveActive)
        {
            isWaveActive = true;
            waveStartTime = Time.time;
        }
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
    }

    // Nouvelle méthode pour changer de vague avec les flèches haut et bas
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
