using UnityEngine;

// 系统：表现（Presentation）
// 职责：监听生命变化，通过缩放 Fill Transform 显示世界血条比例。
// 依赖：CharacterHealth、血条 Fill Transform。
// 扩展：血条颜色、缓动和受击闪烁放在这里；生命规则放在 CharacterHealth。
public class HealthFillView : MonoBehaviour
{
    [SerializeField] private CharacterHealth characterHealth;
    [SerializeField] private Transform healthFillTransform;

    private Vector3 originalScale;
    private Vector3 originalLocalPosition;

    private void Awake()
    {
        if (characterHealth == null)
        {
            characterHealth = GetComponentInParent<CharacterHealth>();
        }

        if (healthFillTransform != null)
        {
            originalScale = healthFillTransform.localScale;
            originalLocalPosition = healthFillTransform.localPosition;
        }
    }

    private void OnEnable()
    {
        if (characterHealth != null)
        {
            characterHealth.OnHealthChanged += RefreshFromHealthChange;
        }
    }

    private void OnDisable()
    {
        if (characterHealth != null)
        {
            characterHealth.OnHealthChanged -= RefreshFromHealthChange;
        }
    }

    private void Start()
    {
        RefreshHealthFill();
    }

    private void RefreshFromHealthChange(CharacterHealth health)
    {
        RefreshHealthFill();
    }

    private void RefreshHealthFill()
    {
        if (characterHealth == null || healthFillTransform == null)
        {
            return;
        }

        float ratio = characterHealth.MaxHealth > 0
            ? (float)characterHealth.CurrentHealth / characterHealth.MaxHealth
            : 0f;

        ratio = Mathf.Clamp01(ratio);
        healthFillTransform.localScale = originalScale * ratio;
        healthFillTransform.localPosition = originalLocalPosition;
    }
}
