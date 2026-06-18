using UnityEngine;

// 系统：战斗（Combat）
// 职责：查询攻击半径内的碰撞体，不判断是否命中，也不施加伤害。
// 依赖：Physics2D、LayerMask。
// 扩展：目标筛选、非分配查询等“物理搜索优化”放在这里；形状判断放在对应范围规则中。
public static class AttackTargetFinder
{
    public static Collider2D[] FindTargets(Vector2 origin, float range, LayerMask targetLayer)
    {
        return Physics2D.OverlapCircleAll(origin, range, targetLayer);
    }
}
