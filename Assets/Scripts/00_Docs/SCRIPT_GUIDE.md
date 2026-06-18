# StartFight 脚本放置指南

## 1. 目录规则

- `Game`：暂停、结束、重新开始等全局流程。
- `Character`：玩家和敌人都能使用的生命、死亡、等级和基础数值。
- `Player`：只有玩家拥有的输入响应、移动、攻击入口和经验入口。
- `Enemy`：敌人生成、追踪和敌人专属攻击入口。
- `Combat`：双方共用的目标查找、命中判断和伤害结算。
- `Reward`：经验和未来掉落。
- `Input`：把设备输入转换为通用输入数据，不执行玩法。
- `Presentation`：HUD、世界血条、特效、相机和伤害数字。
- `00_Docs`：架构、脚本说明和开发路线。

## 2. 脚本职责速查

| 想修改的内容 | 应查看的脚本 |
| --- | --- |
| 玩家移动 | `PlayerMovement` |
| 玩家攻击触发流程 | `PlayerAttack` |
| 扇形是否命中 | `AttackCone` |
| 范围内有哪些目标 | `AttackTargetFinder` |
| 如何统一扣血 | `DamageDealer`、`CharacterHealth` |
| 角色什么时候死亡 | `CharacterDeath` |
| 攻击力、最大生命、经验公式 | `CharacterStats` |
| 玩家经验与升级 | `PlayerExperience`、`CharacterLevel` |
| 击杀经验发放 | `ExpReward` |
| 敌人生成 | `EnemySpawner` |
| 敌人追踪 | `EnemyMovement` |
| 敌人接触伤害 | `EnemyContactDamage` |
| HUD | `PlayerStatsUI` |
| 世界血条 | `HealthFillView` |
| 攻击扇形显示 | `FanAttackVisual` |
| 游戏暂停和结束 | `GameManager`、`GameState` |

## 3. 新功能放置示例

- 攻击冷却：放在 `PlayerAttack` 附近；若多种攻击共用，再迁移到 Combat。
- 敌人攻击玩家：从 `EnemyContactDamage` 开始，通过 `DamageDealer` 扣血。
- 护甲、暴击：扩展 `DamageInfo` 和 `DamageDealer`。
- 经验倍率：放在 Reward 或 `PlayerExperience`，不要放进 UI。
- 新掉落：在 Reward 新建脚本，不要放进 `EnemyMovement`。
- 游戏结束：由玩家的 `CharacterDeath` 触发 `GameManager.GameOver`，接线时不要让 `CharacterHealth` 直接操作 UI。
- 伤害数字：让伤害结果通知 `DamageNumberView`，显示脚本不参与计算。

## 4. 禁止错误放置

- 不在 Presentation 计算伤害、经验或死亡。
- 不在 Input 直接移动玩家或攻击敌人。
- 不在 `GameManager` 保存角色攻击、敌人生成或 HUD 引用。
- 不在 `CharacterStats` 查找敌人、播放特效或发放奖励。
- 不让 `EnemySpawner` 控制已生成敌人的移动和战斗细节。
- 不为只有一个实现的行为提前增加接口或抽象基类。

## 5. Inspector 绑定

### Player

应挂载：`Rigidbody2D`、`CharacterStats`、`CharacterHealth`、`CharacterDeath`、`CharacterLevel`、`PlayerExperience`、`PlayerMovement`、`PlayerAttack`。

- `CharacterHealth.characterStats` → Player 的 `CharacterStats`
- `CharacterHealth.characterDeath` → Player 的 `CharacterDeath`
- `CharacterLevel.characterStats` → Player 的 `CharacterStats`
- `CharacterLevel.characterHealth` → Player 的 `CharacterHealth`
- `PlayerExperience.characterLevel` → Player 的 `CharacterLevel`
- `PlayerMovement.moveJoystick` → 左侧移动摇杆
- `PlayerAttack.attackJoystick` → 右侧攻击摇杆
- `PlayerAttack.playerStats` → Player 的 `CharacterStats`
- `PlayerAttack.playerExperience` → Player 的 `PlayerExperience`
- `PlayerAttack.enemyLayer` → Enemy Layer
- `PlayerAttack.fanAttackVisual` → AttackFanVisual Prefab
- `PlayerAttack.visualRoot` → 可选的攻击视觉父节点

同对象组件引用大多可由 `GetComponent` 自动补齐，但 Inspector 显式绑定更容易检查。

### Enemy Prefab

应挂载：`Rigidbody2D`、`CharacterStats`、`CharacterHealth`、`CharacterDeath`、`EnemyMovement`、`HealthFillView`。

- `CharacterHealth.characterStats` → 本 Prefab 的 `CharacterStats`
- `CharacterHealth.characterDeath` → 本 Prefab 的 `CharacterDeath`
- `HealthFillView.characterHealth` → 本 Prefab 的 `CharacterHealth`
- `HealthFillView.healthFillTransform` → 血条 Fill Transform
- `EnemyContactDamage` → 当前不要挂载；启用敌人攻击时再配置 Target Layer 和伤害值

### Scene objects

- `EnemySpawner.playerTransform` → Player Transform
- `EnemySpawner.enemyPrefabs` → 所有 Enemy Prefab
- `PlayerStatsUI.playerStats` → Player `CharacterStats`
- `PlayerStatsUI.playerHealth` → Player `CharacterHealth`
- `PlayerStatsUI.playerExperience` → Player `PlayerExperience`
- `PlayerStatsUI` 四个文本字段 → 对应 TMP 文本
- `CameraFollow.target` → Player Transform

## 6. 命名规则

- 组件类使用名词或“对象 + 行为”：`CharacterHealth`、`EnemyMovement`。
- 入口动作使用明确动词：`PerformAttack`、`GainExp`、`TryDealDamage`。
- 返回是否成功的方法使用 `Try`；布尔判断使用 `Is`。
- 显示脚本以 `View` 或 `UI` 结尾。
- 不使用含义宽泛的 `Controller`、`Helper`、`Utility` 或 `Manager`；只有真正拥有全局游戏状态的 `GameManager` 例外。
- 每个脚本顶部保留四行中文身份注释：`系统`、`职责`、`依赖`、`扩展`。
