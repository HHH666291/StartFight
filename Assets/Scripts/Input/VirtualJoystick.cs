using System;
using UnityEngine;
using UnityEngine.EventSystems;

// 系统：输入（Input）
// 职责：把触摸或鼠标拖动转换为标准化方向，并在释放时发布事件。
// 依赖：Canvas、RectTransform、Unity EventSystem。
// 扩展：摇杆死区、灵敏度等纯输入转换放在这里；移动和攻击玩法规则放在 Player 系统。
public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI")]
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [Header("Settings")]
    [SerializeField] private float handleRange = 100f;
    public Vector2 Direction { get; private set; }
    private Canvas canvas;
    private Camera cam;

    public event Action<Vector2> OnJoystickReleased;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            cam = canvas.worldCamera;
        }
        ResetJoystick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        UpdateJoystick(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 releasedDirection = Direction;
        ResetJoystick();

        if (releasedDirection.sqrMagnitude > 0.1f)
        {
            OnJoystickReleased?.Invoke(releasedDirection);
        }
    }
    private void UpdateJoystick(PointerEventData eventData)
    {
        if (background == null || handle == null)
        {
            return;
        }

        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, cam, out position))
        {
            // 将局部指针坐标转换为以中心为原点的 -1 到 1 范围。
            position.x = (position.x / background.sizeDelta.x) * 2f;
            position.y = (position.y / background.sizeDelta.y) * 2f;

            Vector2 clampedPosition = Vector2.ClampMagnitude(position, 1f);

            Direction = new Vector2(clampedPosition.x, clampedPosition.y).normalized;

            handle.anchoredPosition = new Vector2(clampedPosition.x * handleRange, clampedPosition.y * handleRange);
        }
    }

    private void ResetJoystick()
    {
        Direction = Vector2.zero;
        if (handle != null)
        {
            handle.anchoredPosition = Vector2.zero;
        }
    }


}
