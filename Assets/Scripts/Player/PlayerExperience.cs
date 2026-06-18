using System;
using UnityEngine;

// System: Player
// Role: Provides the player-facing experience API and relays progression events to the HUD.
// Depends on: CharacterLevel.
public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private CharacterLevel characterLevel;

    public int CurrentExp => characterLevel != null ? characterLevel.CurrentExp : 0;
    public int ExpToNextLevel => characterLevel != null ? characterLevel.ExpToNextLevel : 0;

    public event Action<PlayerExperience> OnExperienceChanged;
    public event Action<PlayerExperience> OnLevelChanged;

    private void Awake()
    {
        if (characterLevel == null)
        {
            characterLevel = GetComponent<CharacterLevel>();
        }
    }

    private void OnEnable()
    {
        if (characterLevel != null)
        {
            characterLevel.OnExperienceChanged += RelayExperienceChanged;
            characterLevel.OnLevelChanged += RelayLevelChanged;
        }
    }

    private void OnDisable()
    {
        if (characterLevel != null)
        {
            characterLevel.OnExperienceChanged -= RelayExperienceChanged;
            characterLevel.OnLevelChanged -= RelayLevelChanged;
        }
    }

    public void GainExp(int amount)
    {
        characterLevel?.GainExp(amount);
    }

    private void RelayExperienceChanged(CharacterLevel levelProgress)
    {
        OnExperienceChanged?.Invoke(this);
    }

    private void RelayLevelChanged(CharacterLevel levelProgress)
    {
        OnLevelChanged?.Invoke(this);
    }
}
