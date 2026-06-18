using UnityEngine;

// 系统：表现（Presentation）
// 职责：让相机以固定偏移平滑跟随目标。
// 依赖：目标 Transform。
// 扩展：镜头偏移、震动、缩放等纯表现功能放在这里，不修改角色玩法状态。
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
