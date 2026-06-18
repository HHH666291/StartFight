using TMPro;
using UnityEngine;

// 系统：表现（Presentation）
// 职责：显示伤害数字，不计算伤害；当前作为未来接入点保留。
// 依赖：TextMeshProUGUI。
// 扩展：飘字动画、颜色和暴击样式放在这里；伤害计算放在 Combat 系统。
public class DamageNumberView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;

    public void ShowDamage(int amount)
    {
        if (damageText != null)
        {
            damageText.text = amount.ToString();
        }
    }
}
