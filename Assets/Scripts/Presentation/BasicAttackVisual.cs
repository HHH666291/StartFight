using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackVisual : MonoBehaviour
{
    [SerializeField] private Transform aimRoot;
    [SerializeField] private Animator animator;

    [SerializeField] private float angleOffset;

    private static readonly int AttackHash = Animator.StringToHash("Attack");

    public void PlayTowards(Vector3 targetPosition)
    {
        Vector2 direction = targetPosition - aimRoot.position;

        if (direction.sqrMagnitude < 0.01f)
        {
            return;

        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        aimRoot.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
        animator.ResetTrigger(AttackHash);
        animator.SetTrigger(AttackHash);
    }
}
