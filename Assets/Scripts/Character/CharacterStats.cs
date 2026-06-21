using UnityEngine;

// 系统：角色（Character）
// 职责：保存角色等级，并统一计算最大生命、攻击力、升级经验和击杀奖励。
// 依赖：仅依赖 Unity 数学工具，不依赖生命、死亡或经验组件。
// 扩展：新增力量、护甲、成长曲线等“角色基础数值”时放在这里；当前生命等运行时状态不要放进来。
public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int level = 1;

    public int Level => level;
    public int MaxHealth => GetMaxHealth(level);
    public int AttackPower => GetAttackPower(level);
    public int ExpToNextLevel => GetExpToNextLevel(level);
    public int ExpReward => GetExpReward(level);

    internal void IncreaseLevel()
    {
        SetLevel(Level+1);
    }

    public void SetLevel(int value)
    {
        level = ClampLevel (value );
    }
    private static int ClampLevel(int value)
    {
        return Mathf.Clamp(value, 1, 999);
    }

    private static int GetMaxHealth(int value)
    {
        value = ClampLevel(value);
        return 8 + (value - 1) * 20;
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
