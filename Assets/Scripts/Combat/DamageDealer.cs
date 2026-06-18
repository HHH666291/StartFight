// System: Combat
// Role: Validates a DamageInfo request and applies it through CharacterHealth.
// Depends on: DamageInfo, CharacterHealth.
public static class DamageDealer
{
    public static bool TryDealDamage(DamageInfo damage)
    {
        if (damage.Target == null || damage.Amount <= 0)
        {
            return false;
        }

        CharacterHealth targetHealth = damage.Target.Health;
        return targetHealth != null && targetHealth.TakeDamage(damage.Amount);
    }
}
