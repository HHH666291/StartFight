using UnityEngine;

// 系统：表现（Presentation）
// 职责：生成并显示临时扇形攻击网格，到期后自动销毁。
// 依赖：MeshFilter、MeshRenderer。
// 扩展：扇形材质、动画和生命周期放在这里；命中判定仍由 Combat 系统负责。
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class FanAttackVisual : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private int segmentCount = 10;

    private MeshFilter meshFilter;
    private float timer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    public void InitializeAttackVisual(Vector2 direction, float range, float angle)
    {
        BuildFanMesh(range, angle);
        RotateToDirection(direction);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void BuildFanMesh(float range, float angle)
    {
        // 第 0 个顶点是圆心，其余顶点依次描出扇形圆弧。
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[segmentCount + 2];
        int[] triangles = new int[segmentCount * 3];

        vertices[0] = Vector3.zero;

        float halfAngle = angle / 2f;
        for (int i = 0; i <= segmentCount; i++)
        {
            float currentAngle = -halfAngle + (angle * i / segmentCount);
            float radianAngle = currentAngle * Mathf.Deg2Rad;
            vertices[i + 1] = new Vector3(Mathf.Sin(radianAngle) * range, Mathf.Cos(radianAngle) * range, 0f);
        }

        for (int i = 0; i < segmentCount; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }

    private void RotateToDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
