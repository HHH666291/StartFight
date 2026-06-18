using UnityEngine;

// 系统：敌人（Enemy）
// 职责：定时在玩家周围生成敌人，并为新敌人设置追踪目标。
// 依赖：EnemyMovement、敌人预制体、玩家 Transform。
// 扩展：波次、权重、生成上限等“刷怪调度”放在这里；敌人自身行为放在对应 Enemy 组件。
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
