using UnityEngine;

// System: Enemy
// Role: Periodically spawns enemy prefabs around the player and assigns their movement target.
// Depends on: EnemyMovement, enemy prefabs, player Transform.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 10f;
    private float spawnTimer;

    private void Update()
    {
        if (playerTransform == null || enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            return;
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float spawnDistance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = (Vector2)playerTransform.position + UnityEngine.Random.insideUnitCircle * spawnDistance;
        GameObject enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetTarget(playerTransform);
        }
    }
}
