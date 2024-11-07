using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.ProBuilder;

// Credits/Resource for mesh generation:
// https://youtu.be/-3ekimUWb9I?feature=shared
// https://www.youtube.com/watch?v=eJEpeUH1EMg
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;
    private bool hasBoxCollider = false;


    [SerializeField] private Vector2 planeSize = new Vector2(1,1);
    [SerializeField] private int planeResolution = 1;

    private List<Vector3> vertices;
    private List<int> triangles;


    private void Awake()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    private void Update()
    {
        planeResolution = Mathf.Clamp(planeResolution, 1, 50);

        GeneratePlane(planeSize, planeResolution);
        SineWave(Time.timeSinceLevelLoad);
        AssignMesh();

        if (!hasBoxCollider)
        {
            gameObject.AddComponent<BoxCollider>();
            hasBoxCollider = true;
        }
    }

    private void GeneratePlane(Vector2 size, int resolution)
    {
        // create vertices
        vertices = new List<Vector3>();
        float xPerStep = size.x / resolution;
        float yPerStep = size.y / resolution; 
        for (int y = 0; y < resolution + 1; y++)
        {
            for (int x = 0; x < resolution + 1; x++)
            {
                vertices.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
            }
        }

        // create triangles
        triangles = new List<int>();
        for (int row = 0; row < resolution; row++)
        {
            for (int col = 0; col < resolution; col++)
            {
                int i = (row * resolution) + row + col;

                // triangles are drawn clockwise, two triangles making up a square

                // first triangle
                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);

                // second triangle
                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);

            }
        }
    }

    private void AssignMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    // increases the vertex's y position based on a sin function and time
    private void SineWave(float time)
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertex = vertices[i];
            vertex.y = Mathf.Sin(time + vertex.x) / 5;
            vertices[i] = vertex;
        }
    }
}
