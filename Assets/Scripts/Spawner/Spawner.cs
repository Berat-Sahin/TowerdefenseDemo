using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawner : MonoBehaviour
{
   
    public static Action OnWaveCompleted;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;

     [SerializeField] private ObjectPooler _pooler;

    
    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRemaining;

    private Waypoint _waypoint;

    private int TotalSpawnCount;


    private void Start()
    {

        _pooler=GetComponent<ObjectPooler>();
        _waypoint = GetComponent<Waypoint>();

        _enemiesRemaining = enemyCount;

        TotalSpawnCount=0;
    }

     private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                TotalSpawnCount++;
                SpawnEnemy();
            }
        }

    


    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }


     private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        enemyCount=enemyCount+1;
        _enemiesRemaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRemaining--;
        if (_enemiesRemaining <= 0)
        {
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());
        }
    }


    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
      
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
 
    }
 

}
