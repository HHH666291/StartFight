// 系统：战斗（Combat）
// 职责：统一校验伤害请求，并把有效伤害施加给目标生命组件。
// 依赖：DamageInfo、CharacterHealth。
// 扩展：护甲、暴击、抗性等“通用伤害结算规则”放在这里。
public static class DamageDealer
{
    public static bool TryDealDamage(DamageInfo damage)
    {
        if (damage.Target == null || damage.Amount <= 0)
        {
            return false;
        }

        return damage.Target.TakeDamage(damage.Amount);
    }
}
