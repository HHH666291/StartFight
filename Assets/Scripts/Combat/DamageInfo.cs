// System: Combat
// Role: Describes one damage request with amount, source, and target.
// Depends on: CharacterStats.
public readonly struct DamageInfo
{
    public int Amount { get; }
    public CharacterStats Source { get; }
    public CharacterStats Target { get; }

    public DamageInfo(int amount, CharacterStats source, CharacterStats target)
    {
        Amount = amount;
        Source = source;
        Target = target;
    }
}
