using System;
using UnityEngine;

// 系统：角色（Character）
// 职责：保存当前经验，判断升级，并协调等级增长和升级回满生命。
// 依赖：CharacterStats 提供等级/经验阈值；CharacterHealth 执行升级回满。
// 扩展：连续升级、升级事件和通用经验规则放在这里；玩家专属经验入口放在 PlayerExperience。
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
    }
}
