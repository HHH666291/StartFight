using System;
using UnityEngine;

// System: Character
// Role: Exposes shared character stats and keeps compatibility access to health and leveling.
// Depends on: CharacterHealth, CharacterDeath, CharacterLevel.
public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private CharacterHealth characterHealth;
    [SerializeField] private CharacterDeath characterDeath;
    [SerializeField] private CharacterLevel characterLevel;

    public int Level => level;
    public int CurrentHealth => characterHealth != null ? characterHealth.CurrentHealth : 0;
    public int CurrentExp => characterLevel != null ? characterLevel.CurrentExp : 0;

    public int MaxHealth => GetMaxHealth(level);
    public int AttackPower => GetAttackPower(level);
    public int ExpToNextLevel => GetExpToNextLevel(level);
    public int ExpReward => GetExpReward(level);

    public bool IsDead => characterDeath != null ? characterDeath.IsDead : CurrentHealth <= 0;
    public CharacterHealth Health => characterHealth;
    public CharacterDeath Death => characterDeath;
    public CharacterLevel LevelProgress => characterLevel;

    public event Action<CharacterStats> OnHealthChanged;
    public event Action<CharacterStats> OnLevelUp;
    public event Action<CharacterStats> OnDeath;
    public event Action<CharacterStats> OnExpChanged;

    private void Awake()
    {
        if (characterHealth == null)
        {
            characterHealth = GetComponent<CharacterHealth>();
        }

        if (characterDeath == null)
        {
            characterDeath = GetComponent<CharacterDeath>();
        }

        if (characterLevel == null)
        {
            characterLevel = GetComponent<CharacterLevel>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (characterHealth != null)
        {
            characterHealth.TakeDamage(damage);
        }
    }

    public void GainExp(int exp)
    {
        if (characterLevel != null)
        {
            characterLevel.GainExp(exp);
        }
    }

    internal void IncreaseLevel()
    {
        level++;
    }

    internal void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(this);
    }

    internal void NotifyLevelChanged()
    {
        OnLevelUp?.Invoke(this);
    }

    internal void NotifyDeath()
    {
        OnDeath?.Invoke(this);
    }

    internal void NotifyExperienceChanged()
    {
        OnExpChanged?.Invoke(this);
    }

    private static int ClampLevel(int value)
    {
        return Mathf.Clamp(value, 1, 999);
    }

    private static int GetMaxHealth(int value)
    {
        value = ClampLevel(value);
        return 8 + (value - 1) * 2;
    }

    private static int GetAttackPower(int value)
    {
        value = ClampLevel(value);
        return 1 + (value - 1) * 2;
    }

    private static int GetExpToNextLevel(int value)
    {
        value = ClampLevel(value);
        return 5 + (value - 1) * 3;
    }

    private static int GetExpReward(int value)
    {
        value = ClampLevel(value);
        return 2 + (value - 1) * 2;
    }
}
