// 系统：奖励（Reward）
// 职责：确认本次伤害击杀目标后，把目标的经验奖励发给玩家。
// 依赖：DamageInfo、CharacterHealth、CharacterStats、PlayerExperience。
// 扩展：金币、掉落物等新增为 Reward 下的并列规则，不塞进死亡或伤害组件。
public static class ExpReward
{
    public static bool TryGrantKillExperience(DamageInfo damage, PlayerExperience playerExperience)
    {
        if (playerExperience == null || damage.Target == null || !damage.Target.IsDead || damage.Target.Stats == null)
        {
            return false;
        }

        playerExperience.GainExp(damage.Target.Stats.ExpReward);
        return true;
    }
}
