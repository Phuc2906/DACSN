using UnityEngine;
using System.Collections;

public class Spawn_Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;       
    public float spawnRadius = 3f;        
    public int spawnCount = 5;          
    public float spawnInterval = 5f;      
    public LayerMask obstacleMask;        

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (this == null || gameObject == null) yield break;

            if (enemyPrefab == null)
            {
                yield break;
            }

            yield return new WaitForSeconds(spawnInterval);

            SpawnEnemiesAroundBoss();
        }
    }

    void SpawnEnemiesAroundBoss()
    {
        if (enemyPrefab == null) return; 

        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 spawnPos;
            int attempts = 0;

            do
            {
                attempts++;

                Vector2 randomDir = Random.insideUnitCircle.normalized;
                float randomDist = Random.Range(1f, spawnRadius);

                spawnPos = (Vector2)transform.position + randomDir * randomDist;

            } while (Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleMask) && attempts < 10);

            if (enemyPrefab != null)
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
