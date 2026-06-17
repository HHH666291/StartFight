using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private int level = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentExp;

    public int Level => level;
    public int CurrentHealth => currentHealth;
    public int CurrentExp => currentExp;

    public int MaxHealth => CharacterFormula.GetMaxHealth(level);
    public int AttackPower => CharacterFormula.GetAttackPower(level);
    public int ExpToNextLevel => CharacterFormula.GetExpToNextLevel(level);
    public int ExpReward => CharacterFormula.GetExpReward(level);

    public bool IsDead => currentHealth <= 0;

    public event Action<CharacterStats> OnHealthChanged;
    public event Action<CharacterStats> OnLevelUp;
    public event Action<CharacterStats> OnDeath;
    public event Action<CharacterStats> OnExpChanged;

    private void Awake()
    {
        currentHealth = MaxHealth;
        currentExp = 0;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }
        currentHealth -= damage;
        OnHealthChanged?.Invoke(this);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void GainExp(int exp)
    {
        if (isPlayer)
        {
            currentExp += exp;
            OnExpChanged?.Invoke(this);
            while (currentExp >= ExpToNextLevel)
            {
                currentExp -= ExpToNextLevel;
                LevelUp();
            }
        }
    }

    private void LevelUp()
    {
        level++;

        currentHealth = MaxHealth; // Restore health on level up
        OnLevelUp?.Invoke(this);
        OnHealthChanged ?.Invoke(this);
    }
}
