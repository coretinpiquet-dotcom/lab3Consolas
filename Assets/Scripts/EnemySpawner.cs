using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            enemy.transform.parent = spawnPoint.transform;
        }
    }
}
