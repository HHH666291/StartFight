using System;
using UnityEngine;

// 系统：玩家（Player）
// 职责：提供玩家专属的经验入口，并把通用升级事件转发给玩家 HUD。
// 依赖：CharacterLevel。
// 扩展：玩家经验倍率、玩家专属加成等放在这里；通用升级计算仍放在 CharacterLevel。
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
