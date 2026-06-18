using UnityEngine;

// System: Presentation
// Role: Displays CharacterHealth as a scaled fill Transform.
// Depends on: CharacterHealth, fill Transform.
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
