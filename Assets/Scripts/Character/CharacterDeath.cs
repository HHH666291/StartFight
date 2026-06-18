using System;
using UnityEngine;

// 系统：角色（Character）
// 职责：保存死亡状态、发布死亡事件，并移除死亡角色对象。
// 依赖：无业务组件依赖，由 CharacterHealth 在生命归零时调用。
// 扩展：死亡动画、延迟销毁或对象池回收放在这里；奖励发放放在 Reward 系统。
public class CharacterDeath : MonoBehaviour
{
    public bool IsDead { get; private set; }

    public event Action<CharacterDeath> OnDeath;

    public void Die()
    {
        if (IsDead)
        {
            return;
        }

        IsDead = true;
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
