using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class FanAttackVisual : MonoBehaviour
{
    [SerializeField ]private float lifeTime = 0.5f;
    [SerializeField ]private int segmentCount = 10;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private float timer;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Init(Vector2 direction, float range, float angle)
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
