using UnityEngine;

// System: Combat
// Role: Finds colliders inside an attack radius without deciding hits or applying damage.
// Depends on: Physics2D, LayerMask.
public static class AttackTargetFinder
{
    public static Collider2D[] FindTargets(Vector2 origin, float range, LayerMask targetLayer)
    {
        return Physics2D.OverlapCircleAll(origin, range, targetLayer);
    }
}
