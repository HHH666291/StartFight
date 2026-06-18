// 系统：战斗（Combat）
// 职责：描述一次伤害请求，只携带伤害值、来源数值和目标生命。
// 依赖：CharacterStats 表示伤害来源；CharacterHealth 表示承受伤害的目标。
// 扩展：暴击、伤害类型、击退等“一次攻击的数据”加在这里，不在此处执行规则。
public readonly struct DamageInfo
{
    public int Amount { get; }
    public CharacterStats Source { get; }
    public CharacterHealth Target { get; }

    public DamageInfo(int amount, CharacterStats source, CharacterHealth target)
    {
        Amount = amount;
        Source = source;
        Target = target;
    }
}
