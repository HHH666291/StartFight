using UnityEngine;

// 系统：战斗（Combat）
// 职责：判断一个世界坐标是否位于指定方向的扇形攻击范围内。
// 依赖：UnityEngine 向量数学。
// 扩展：只放扇形判定数学；圆形、直线等攻击形状在 Combat 下新增并列规则。
public static class AttackCone
{
    public static bool IsInCone(Vector2 origin, Vector2 direction, Vector2 targetPosition, float coneAngle)
    {
        Vector2 toTarget = targetPosition - origin;
        if (toTarget.sqrMagnitude < 0.01f)
        {
            return false;
        }

        // 目标方向与攻击方向的夹角不超过扇形半角时，目标位于扇形内。
        float angleToTarget = Vector2.Angle(direction, toTarget);
        return angleToTarget <= coneAngle / 2f;
    }
}
