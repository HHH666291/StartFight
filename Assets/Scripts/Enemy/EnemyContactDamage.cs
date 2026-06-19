using UnityEngine;

// 系统：敌人（Enemy）
// 职责：提供可选的敌人接触伤害入口；当前作为未来敌人攻击扩展点保留。
// 依赖：CharacterStats、CharacterHealth、DamageDealer。
// 扩展：敌人接触攻击参数和触发条件放在这里；通用伤害计算仍放在 DamageDealer。
public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private CharacterStats enemyStats;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField, Min(1)] private int contactDamage = 1;
    [SerializeField, Min(0.1f)] private float attackInterval = 3f;

    private float nextAttackTime;
    private void Awake()
    {
        if (enemyStats == null)
        {
            enemyStats = GetComponent<CharacterStats>();
        }
    }

    //碰撞攻击
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((targetLayer.value & (1 << collision.gameObject.layer)) == 0)
        {
            return;
        }

        CharacterHealth targetHealth = collision.gameObject.GetComponent<CharacterHealth>();

        if (targetHealth == null) return;
        if (Time.time < nextAttackTime) return;

        if (DamageDealer.TryDealDamage(new DamageInfo(contactDamage, enemyStats, targetHealth)))
        {
            nextAttackTime = Time.time+attackInterval ;
        }

    }

}
