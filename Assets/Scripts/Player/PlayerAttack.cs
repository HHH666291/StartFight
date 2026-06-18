using UnityEngine;

// 系统：玩家（Player）
// 职责：响应攻击输入，协调目标查找、范围判断、伤害、击杀奖励和攻击表现。
// 依赖：VirtualJoystick、CharacterStats、PlayerExperience、Combat、FanAttackVisual。
// 扩展：玩家攻击流程和技能释放入口放在这里；范围数学与伤害规则分别放在 Combat 对应脚本。
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private VirtualJoystick attackJoystick;
    [SerializeField] private CharacterStats playerStats;
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackAngle = 45f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private FanAttackVisual fanAttackVisual;
    [SerializeField] private Transform visualRoot;

    private void Awake()
    {
        if (playerStats == null)
        {
            playerStats = GetComponent<CharacterStats>();
        }

        if (playerExperience == null)
        {
            playerExperience = GetComponent<PlayerExperience>();
        }
    }

    private void OnEnable()
    {
        if (attackJoystick != null)
        {
            attackJoystick.OnJoystickReleased += PerformAttack;
        }
    }

    private void OnDisable()
    {
        if (attackJoystick != null)
        {
            attackJoystick.OnJoystickReleased -= PerformAttack;
        }
    }

    private void PerformAttack(Vector2 direction)
    {
        if (playerStats == null || direction.sqrMagnitude < 0.1f)
        {
            return;
        }

        Vector2 attackDirection = direction.normalized;
        ShowAttackVisual(attackDirection);

        Collider2D[] nearbyTargets = AttackTargetFinder.FindTargets(transform.position, attackRange, enemyLayer);
        foreach (Collider2D targetCollider in nearbyTargets)
        {
            TryAttackTarget(targetCollider, attackDirection);
        }
    }

    private void TryAttackTarget(Collider2D targetCollider, Vector2 attackDirection)
    {
        Vector2 targetPosition = targetCollider.transform.position;
        if (!AttackCone.IsInCone(transform.position, attackDirection, targetPosition, attackAngle))
        {
            return;
        }

        CharacterHealth targetHealth = targetCollider.GetComponent<CharacterHealth>();
        if (targetHealth == null)
        {
            return;
        }

        DamageInfo damage = new DamageInfo(playerStats.AttackPower, playerStats, targetHealth);
        if (DamageDealer.TryDealDamage(damage))
        {
            ExpReward.TryGrantKillExperience(damage, playerExperience);
        }
    }

    private void ShowAttackVisual(Vector2 attackDirection)
    {
        if (fanAttackVisual == null)
        {
            return;
        }

        FanAttackVisual visual = Instantiate(fanAttackVisual, transform.position, Quaternion.identity, visualRoot);
        visual.InitializeAttackVisual(attackDirection, attackRange, attackAngle);
    }
}
