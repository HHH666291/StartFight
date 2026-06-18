using UnityEngine;

// System: Enemy
// Role: Moves an enemy Rigidbody2D toward its assigned target.
// Depends on: Rigidbody2D, Transform target.
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetTarget(Transform playerTransform)
    {
        target = playerTransform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

}
