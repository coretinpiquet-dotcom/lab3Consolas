using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] buffPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnBuffs();
    }

    private void SpawnBuffs()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            GameObject buffPrefab = buffPrefabs[Random.Range(0, buffPrefabs.Length)];
            GameObject enemy = Instantiate(buffPrefab, spawnPoint.position, spawnPoint.rotation);
            enemy.transform.parent = spawnPoint.transform;
        }
    }
}
