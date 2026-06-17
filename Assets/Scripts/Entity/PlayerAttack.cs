using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private VirtualJoystick attackJoystick;
    [SerializeField] private CharacterStats playerStats;
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
    }

    private void OnEnable()
    {
        if (attackJoystick != null)
        {
            attackJoystick.OnJoystickReleased += HandleAttack;
        }
    }

    private void OnDisable()
    {
        if (attackJoystick != null)
        {
            attackJoystick.OnJoystickReleased -= HandleAttack;
        }
    }

    private void HandleAttack(Vector2 direction)
    {
        Debug.Log("攻击触发，方向：" + direction);
        if (playerStats == null)
        {
            Debug.LogWarning("PlayerStats is not assigned.");
            return;
        }
        if (direction.sqrMagnitude < 0.1f)
        {
            return; // Ignore small inputs
        }
        

        Vector2 attackDirection = direction.normalized;

        ShowAttackVisual(attackDirection);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Vector2 toEnemy = enemy.transform.position - transform.position;
            if (toEnemy.sqrMagnitude < 0.01f)
            {
                continue; // Skip if the enemy is at the same position as the player
            }

            float angleToEnemy = Vector2.Angle(attackDirection, toEnemy);


            if (angleToEnemy <= attackAngle / 2f)
            {
                CharacterStats enemyStats = enemy.GetComponent<CharacterStats>();
                if (enemyStats != null)
                {
                    Debug.Log("玩家攻击力：" + playerStats.AttackPower);
                    Debug.Log("敌人受伤前 HP：" + enemyStats.CurrentHealth + "/" + enemyStats.MaxHealth);

                    enemyStats.TakeDamage(playerStats.AttackPower);

                    Debug.Log("敌人受伤后 HP：" + enemyStats.CurrentHealth + "/" + enemyStats.MaxHealth);
                }
                int expReward = enemyStats != null ? enemyStats.ExpReward : 0;
                if (enemyStats != null && enemyStats.IsDead)
                {
                    playerStats.GainExp(expReward);
                }
            }
        }
    }

    private void ShowAttackVisual(Vector2 attackDirection)
    {
        if(fanAttackVisual == null && visualRoot == null)
        {
            return ;
        }
        Transform parent = visualRoot != null ? visualRoot : null ;

        FanAttackVisual visual = Instantiate(fanAttackVisual, transform.position, Quaternion.identity, parent);
        visual.Init(attackDirection, attackRange, attackAngle);
    }
}
