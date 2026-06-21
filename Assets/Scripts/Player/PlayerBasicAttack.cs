using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float hitDelay = 0.667f;
    [SerializeField] private float attackDuration = 1.167f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private CharacterStats playerStats;
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private BasicAttackVisual attackVisual;

    private bool isAttacking;
    private CharacterHealth lockedTarget;

    private void Awake()
    {
        if (playerStats == null) { playerStats = GetComponent<CharacterStats>(); }
        if (playerExperience == null) { playerExperience = GetComponent<PlayerExperience>(); }

    }

    public void TryBasicAttack()
    {
        if (isAttacking) { return; }
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying) { return; }

        lockedTarget = AttackTargetFinder.FindNearestTarget(transform.position, attackRange, enemyLayer);
        if (lockedTarget == null) return;
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        attackVisual?.PlayTowards(lockedTarget.transform.position);
        yield return new WaitForSeconds(hitDelay);
        ApplyDamageToLockedTarget();
        float recoveryTime = Mathf.Max(0f, attackDuration - hitDelay);

        yield return new WaitForSeconds(recoveryTime);

        lockedTarget = null;
        isAttacking = false;
    }

    private void ApplyDamageToLockedTarget()
    {
        if (lockedTarget == null || lockedTarget.IsDead || playerStats == null)
        {
            return;
        }
        DamageInfo damage = new DamageInfo(playerStats.AttackPower, playerStats, lockedTarget);

        if (DamageDealer.TryDealDamage(damage)) { ExpReward.TryGrantKillExperience(damage, playerExperience); }
    }
}
