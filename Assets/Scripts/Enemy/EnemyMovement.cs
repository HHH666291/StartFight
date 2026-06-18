using UnityEngine;

// 系统：敌人（Enemy）
// 职责：驱动敌人的 Rigidbody2D 朝指定目标移动。
// 依赖：Rigidbody2D、目标 Transform。
// 扩展：追踪、停靠距离、转向等基础移动放在这里；寻路或敌人状态机复杂后再拆独立组件。
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetTarget(Transform playerTransform)
    {
        target = playerTransform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

}
