using System;
using UnityEngine;

// System: Character
// Role: Owns death state, publishes death, and removes the defeated character object.
// Depends on: CharacterStats.
public class CharacterDeath : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;

    public bool IsDead { get; private set; }

    public event Action<CharacterDeath> OnDeath;

    private void Awake()
    {
        if (characterStats == null)
        {
            characterStats = GetComponent<CharacterStats>();
        }
    }

    public void Die()
    {
        if (IsDead)
        {
            return;
        }

        IsDead = true;
        OnDeath?.Invoke(this);
        characterStats?.NotifyDeath();
        Destroy(gameObject);
    }
}
