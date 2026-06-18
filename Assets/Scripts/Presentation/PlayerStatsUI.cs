using TMPro;
using UnityEngine;

// System: Presentation
// Role: Displays player level, health, attack, and experience in the HUD.
// Depends on: CharacterStats, CharacterHealth, PlayerExperience, TMP text fields.
public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private CharacterStats playerStats;
    [SerializeField] private CharacterHealth playerHealth;
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI expText;

    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged += RefreshFromHealthChange;
        }

        if (playerExperience != null)
        {
            playerExperience.OnExperienceChanged += RefreshFromExperienceChange;
            playerExperience.OnLevelChanged += RefreshFromExperienceChange;
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= RefreshFromHealthChange;
        }

        if (playerExperience != null)
        {
            playerExperience.OnExperienceChanged -= RefreshFromExperienceChange;
            playerExperience.OnLevelChanged -= RefreshFromExperienceChange;
        }
    }

    private void Start()
    {
        RefreshStatsText();
    }

    private void RefreshFromHealthChange(CharacterHealth health)
    {
        RefreshStatsText();
    }

    private void RefreshFromExperienceChange(PlayerExperience experience)
    {
        RefreshStatsText();
    }

    private void RefreshStatsText()
    {
        if (playerStats == null || playerHealth == null || playerExperience == null)
        {
            return;
        }

        levelText.text = "Level: " + playerStats.Level;
        healthText.text = "HP: " + playerHealth.CurrentHealth + "/" + playerHealth.MaxHealth;
        attackText.text = "Attack: " + playerStats.AttackPower;
        expText.text = "EXP: " + playerExperience.CurrentExp + "/" + playerExperience.ExpToNextLevel;
    }
}
