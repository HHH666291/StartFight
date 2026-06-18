using System;
using UnityEngine;

// System: Character
// Role: Owns experience progress and applies level-ups to CharacterStats.
// Depends on: CharacterStats, CharacterHealth.
public class CharacterLevel : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private CharacterHealth characterHealth;
    [SerializeField] private int currentExp;

    public int CurrentExp => currentExp;
    public int ExpToNextLevel => characterStats != null ? characterStats.ExpToNextLevel : 0;

    public event Action<CharacterLevel> OnExperienceChanged;
    public event Action<CharacterLevel> OnLevelChanged;

    private void Awake()
    {
        if (characterStats == null)
        {
            characterStats = GetComponent<CharacterStats>();
        }

        if (characterHealth == null)
        {
            characterHealth = GetComponent<CharacterHealth>();
        }

        currentExp = 0;
    }

    public void GainExp(int amount)
    {
        if (characterStats == null)
        {
            return;
        }

        currentExp += amount;
        OnExperienceChanged?.Invoke(this);
        characterStats.NotifyExperienceChanged();

        while (currentExp >= characterStats.ExpToNextLevel)
        {
            currentExp -= characterStats.ExpToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        characterStats.IncreaseLevel();
        characterHealth?.RestoreToFull();
        OnLevelChanged?.Invoke(this);
        characterStats.NotifyLevelChanged();
    }
}
