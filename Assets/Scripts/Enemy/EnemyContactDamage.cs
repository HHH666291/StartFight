using UnityEngine;

// System: Enemy
// Role: Provides an optional contact-damage entry point for future enemy attacks.
// Depends on: CharacterStats, DamageDealer.
public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private CharacterStats enemyStats;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int contactDamage = 1;

    private void Awake()
    {
        if (enemyStats == null)
        {
            enemyStats = GetComponent<CharacterStats>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((targetLayer.value & (1 << collision.gameObject.layer)) == 0)
        {
            return;
        }

        CharacterStats targetStats = collision.gameObject.GetComponent<CharacterStats>();
        if (targetStats != null)
        {
            DamageDealer.TryDealDamage(new DamageInfo(contactDamage, enemyStats, targetStats));
        }
    }
}
