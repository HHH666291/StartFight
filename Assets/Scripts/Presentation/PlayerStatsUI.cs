using TMPro;
using UnityEngine;

// 系统：表现（Presentation）
// 职责：监听玩家数据变化，在 HUD 显示等级、生命、攻击力和经验。
// 依赖：CharacterStats、CharacterHealth、PlayerExperience、TMP 文本。
// 扩展：新增 HUD 字段和显示格式放在这里；不要从 UI 反向修改玩法状态。
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
