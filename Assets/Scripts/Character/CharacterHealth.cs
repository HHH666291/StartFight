using System;
using UnityEngine;

// System: Character
// Role: Owns current health, applies damage, restores health, and publishes health changes.
// Depends on: CharacterStats, CharacterDeath.
public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private CharacterDeath characterDeath;
    [SerializeField] private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => characterStats != null ? characterStats.MaxHealth : 0;
    public bool IsDead => characterDeath != null && characterDeath.IsDead;

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
        characterStats?.NotifyHealthChanged();
    }
}
