// 系统：游戏流程（Game）
// 职责：定义全局游戏流程可能处于的状态。
// 依赖：无。
// 扩展：只有确实影响全局流程的状态才加在这里，角色局部状态使用各自组件管理。
public enum GameState
{
    Playing,
    Paused,
    GameOver
}
