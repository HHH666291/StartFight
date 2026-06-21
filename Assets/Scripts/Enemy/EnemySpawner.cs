using UnityEngine;

// 系统：敌人（Enemy）
// 职责：定时在玩家周围生成敌人，并为新敌人设置追踪目标。
// 依赖：EnemyMovement、敌人预制体、玩家 Transform。
// 扩展：波次、权重、生成上限等“刷怪调度”放在这里；敌人自身行为放在对应 Enemy 组件。
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private CharacterStats playerStats;//玩家的数据
    [SerializeField] private CharacterHealth playerHealth;//玩家的数据
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 10f;
    private float spawnTimer;

    private int levelsDeltaPlayer = 1;
    private float currentSpawnInterval;

    private void Awake()
    {
        if (playerTransform == null) return;
        if(playerStats == null&& playerTransform != null)
        {
            playerStats = playerTransform.GetComponent<CharacterStats>();
        }
        if(playerHealth == null&& playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<CharacterHealth>();
        }
    }
    private void Start()
    {
        currentSpawnInterval = spawnInterval;
    }

    private void Update()
    {
        if (playerTransform == null || enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            return;
        }

        float healthRatio = (float)playerHealth.CurrentHealth / playerStats.MaxHealth;
        currentSpawnInterval = healthRatio > 0.8f ? spawnInterval / 2 : spawnInterval;

        levelsDeltaPlayer = playerHealth.CurrentHealth;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }     
    }

    private void SpawnEnemy()
    {
        float spawnDistance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = (Vector2)playerTransform.position + UnityEngine.Random.insideUnitCircle.normalized  * spawnDistance;
        GameObject enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetTarget(playerTransform);
        }
        InitializeEnemyLevel(enemy );
    }

    private void InitializeEnemyLevel(GameObject enemy)
    {
        CharacterStats enemyStats= enemy.GetComponentInParent<CharacterStats>();
        if (enemyStats == null) return;
        int minLevel = Mathf.Max(1, playerStats .Level -levelsDeltaPlayer );
        int maxLevel = Mathf.Min(999, playerStats .Level +levelsDeltaPlayer );
        int enemyLevel = Random .Range (minLevel, maxLevel+1);
        enemyStats.SetLevel (enemyLevel);

        CharacterHealth enemyHealth = enemy.GetComponentInParent<CharacterHealth>();
        enemyHealth?.RestoreToFull();
    }


}
