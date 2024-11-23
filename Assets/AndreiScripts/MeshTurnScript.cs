using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTurnScript : MonoBehaviour
{
    public float frequency = 1f; // Частота изгиба
    public float amplitude = 0.5f; // Амплитуда изгиба

    void Start()
    {
        BendMesh();
    }

    void BendMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // Смещение вершин по оси X в зависимости от координаты Z
            vertices[i].x += Mathf.Sin(vertices[i].z * frequency) * amplitude;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Если есть коллайдер, обновляем его
        MeshCollider collider = GetComponent<MeshCollider>();
        if (collider != null)
        {
            collider.sharedMesh = mesh;
        }
    }
}
