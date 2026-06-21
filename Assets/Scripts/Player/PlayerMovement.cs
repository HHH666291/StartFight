using UnityEngine;

// 系统：玩家（Player）
// 职责：读取摇杆或键盘方向，并驱动玩家 Rigidbody2D 移动。
// 依赖：VirtualJoystick、Rigidbody2D。
// 扩展：玩家移动速度、冲刺等移动规则放在这里；输入坐标转换放在 Input 系统。
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private VirtualJoystick moveJoystick;

    private Rigidbody2D rb;
    private bool canMove = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Vector2 input = Vector2.zero;
        if (moveJoystick != null)
        {
            input = moveJoystick.Direction;
        }
        Vector2 keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (keyboardInput.sqrMagnitude > 0.1f)
        {
            input = keyboardInput.normalized;
        }
        rb.velocity = input * moveSpeed;
    }

    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
        if (!canMove && rb != null) { rb.velocity = Vector2.zero; }
    }
}
