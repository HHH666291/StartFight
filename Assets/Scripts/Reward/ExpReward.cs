// System: Reward
// Role: Grants a defeated target's experience reward to the player.
// Depends on: DamageInfo, PlayerExperience.
public static class ExpReward
{
    public static bool TryGrantKillExperience(DamageInfo damage, PlayerExperience playerExperience)
    {
        if (playerExperience == null || damage.Target == null || !damage.Target.IsDead)
        {
            return false;
        }

        playerExperience.GainExp(damage.Target.ExpReward);
        return true;
    }
}
