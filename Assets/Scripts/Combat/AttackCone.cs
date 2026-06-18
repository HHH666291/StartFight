using UnityEngine;

// System: Combat
// Role: Determines whether a world position lies inside a directional attack cone.
// Depends on: UnityEngine vector math.
public static class AttackCone
{
    public static bool IsInCone(Vector2 origin, Vector2 direction, Vector2 targetPosition, float coneAngle)
    {
        Vector2 toTarget = targetPosition - origin;
        if (toTarget.sqrMagnitude < 0.01f)
        {
            return false;
        }

        // A target is inside the fan when its direction is within half the full cone angle.
        float angleToTarget = Vector2.Angle(direction, toTarget);
        return angleToTarget <= coneAngle / 2f;
    }
}
