using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BodyHealthFill : MonoBehaviour
{
    [SerializeField] private CharacterStats playerStats;
    [SerializeField] private Transform healthFillTransform;

    private Vector3 originalScale;
    private Vector3 originalLocalPosition;

    private void Awake()
    {
        if (healthFillTransform != null)
        {
            originalScale = healthFillTransform.localScale;
            originalLocalPosition = healthFillTransform.localPosition;
        }
    }

    private void OnEnable()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChanged += HandleHealthChanged;
        }
    }
    private void OnDisable()
    {
        if (playerStats != null)
        {
            playerStats.OnHealthChanged -= HandleHealthChanged;
        }
    }
    private void Start()
    {
        Refresh();
    }

    private void HandleHealthChanged(CharacterStats stats)
    {
        Refresh();
    }

    private void Refresh()
    {
        if (playerStats == null || healthFillTransform == null)
        {
            return;
        }
        float ratio = 0f;

        if (playerStats.MaxHealth > 0)
        {
            ratio = (float)playerStats.CurrentHealth / playerStats.MaxHealth;
        }

        ratio = Mathf.Clamp01(ratio);

        healthFillTransform.localScale = originalScale * ratio;
        healthFillTransform.localPosition = originalLocalPosition;


    }


}
