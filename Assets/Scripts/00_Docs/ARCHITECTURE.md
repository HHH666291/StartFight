# StartFight 脚本架构

## 1. 系统总览

StartFight 使用按职责分目录的轻量组件结构。没有事件总线、依赖注入、抽象基类或完整框架。MonoBehaviour 负责场景对象行为，少量无状态规则使用静态类。

| 系统 | 负责 | 不负责 |
| --- | --- | --- |
| Game | 全局 Playing、Paused、GameOver 状态和重开入口 | 战斗规则、UI 页面、角色数值 |
| Character | 玩家与敌人共用的数值、生命、死亡和等级进度 | 输入、目标搜索、奖励发放、显示 |
| Player | 玩家移动、攻击入口和玩家经验入口 | 扇形数学、物理搜索、通用伤害规则 |
| Enemy | 敌人移动、生成和可选接触伤害入口 | 玩家经验、HUD、通用伤害实现 |
| Combat | 扇形判定、目标搜索、伤害描述和统一伤害入口 | 输入、奖励、视觉、刷怪 |
| Reward | 击杀经验奖励 | 伤害计算、掉落系统、UI |
| Input | 将触摸或鼠标输入转换为方向和释放事件 | 玩家移动或攻击规则 |
| Presentation | HUD、血条、攻击视觉、相机、伤害数字显示 | 伤害、经验、死亡等玩法规则 |

## 2. 主要调用方向

```text
VirtualJoystick
├─> PlayerMovement ─> Rigidbody2D
└─> PlayerAttack
    ├─> AttackTargetFinder ─> Physics2D
    ├─> AttackCone
    ├─> DamageDealer ─> CharacterHealth ─> CharacterDeath
    ├─> ExpReward ─> PlayerExperience ─> CharacterLevel
    └─> FanAttackVisual

EnemySpawner ─> EnemyMovement
EnemyContactDamage ─> DamageDealer ─> CharacterHealth

CharacterHealth ─> CharacterStats / CharacterDeath
CharacterLevel ─> CharacterStats / CharacterHealth
CharacterHealth ─> HealthFillView / PlayerStatsUI
PlayerExperience ─> PlayerStatsUI
CameraFollow ─> Player Transform
```

依赖方向保持从具体入口指向通用规则，再指向状态组件。`CharacterStats` 不反向引用生命、死亡和经验组件，避免角色组件循环依赖。Presentation 只订阅数据变化，不反向修改玩法。

## 3. 当前脚本列表

### Game

- `GameState`：全局游戏状态枚举。
- `GameManager`：暂停、继续、结束和重开入口。

### Character

- `CharacterStats`：角色等级值和成长公式，不保存当前生命、死亡或当前经验。
- `CharacterHealth`：当前生命、扣血、回满和生命事件。
- `CharacterDeath`：死亡状态、死亡事件和对象销毁。
- `CharacterLevel`：经验累计和升级流程。

### Player

- `PlayerMovement`：读取移动输入并设置速度。
- `PlayerAttack`：协调一次玩家攻击的完整调用流程。
- `PlayerExperience`：玩家专属经验 API 和 HUD 事件。

### Enemy

- `EnemyMovement`：追踪目标。
- `EnemySpawner`：定时生成并设置追踪目标。
- `EnemyContactDamage`：未来接触攻击入口，当前不要求挂载。

### Combat

- `DamageInfo`：一次伤害的数据。
- `DamageDealer`：统一伤害入口。
- `AttackCone`：扇形范围数学判断。
- `AttackTargetFinder`：Physics2D 范围查询。

### Reward

- `ExpReward`：确认击杀后发放目标经验奖励。

### Input

- `VirtualJoystick`：摇杆方向和释放事件。

### Presentation

- `CameraFollow`：相机跟随。
- `FanAttackVisual`：扇形 Mesh 和生命周期。
- `HealthFillView`：生命比例显示。
- `PlayerStatsUI`：玩家 HUD。
- `DamageNumberView`：伤害数字显示入口，目前未接入。

## 4. 当前保留的简化点

- 成长公式仍是 `CharacterStats` 内的简单静态方法，没有配置系统。
- 伤害统一走 `DamageDealer` 并直接面向 `CharacterHealth`；玩家经验统一走 `PlayerExperience`。
- `PlayerExperience` 当前是较薄的玩家专属边界，未来玩家经验倍率或专属规则优先放在这里。
- `DamageDealer`、`AttackCone`、`AttackTargetFinder`、`ExpReward` 无状态，因此使用静态类，避免增加无意义场景组件。
- `GameManager` 只保存游戏状态，不管理 UI、角色、敌人或音频。
- `EnemyContactDamage` 和 `DamageNumberView` 是明确的扩展落点，但当前不挂载、不改变现有玩法。

## 5. 未来继续拆分的时机

- 出现多套成长曲线时，再把数值公式迁移到配置数据。
- 出现多种攻击形状时，再为圆形、直线等判定增加并列规则；当前不要建立技能框架。
- 伤害需要暴击、护甲或属性时，在 `DamageInfo` 和 `DamageDealer` 内扩展。
- 奖励出现经验之外的掉落时，在 Reward 下新增独立掉落脚本，不把掉落塞进 `CharacterDeath`。
- Game 状态需要 UI 响应时，再增加简单状态事件；当前不建立全局事件总线。
