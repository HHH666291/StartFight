using System;
using UnityEngine;

// 系统：角色（Character）
// 职责：保存当前生命，处理扣血/回满，并发布生命变化事件。
// 依赖：CharacterStats 提供最大生命；CharacterDeath 处理死亡结果。
// 扩展：治疗、护盾、无敌等“生命状态规则”放在这里；伤害公式放在 Combat 系统。
public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private CharacterDeath characterDeath;
    [SerializeField] private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => characterStats != null ? characterStats.MaxHealth : 0;
    public bool IsDead => characterDeath != null && characterDeath.IsDead;
    public CharacterStats Stats => characterStats;

    public event Action<CharacterHealth> OnHealthChanged;

    private void Awake()
    {
        if (characterStats == null)
        {
            characterStats = GetComponent<CharacterStats>();
        }

        if (characterDeath == null)
        {
            characterDeath = GetComponent<CharacterDeath>();
        }

        currentHealth = MaxHealth;
    }

    public bool TakeDamage(int damage)
    {
        if (IsDead)
        {
            return false;
        }

        currentHealth -= damage;
        NotifyHealthChanged();

        if (currentHealth <= 0 && characterDeath != null)
        {
            characterDeath.Die();
        }

        return true;
    }

    public void RestoreToFull()
    {
        currentHealth = MaxHealth;
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(this);
    }
}
