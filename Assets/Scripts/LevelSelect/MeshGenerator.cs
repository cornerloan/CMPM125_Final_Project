using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Credits/Resource for mesh generation:
// https://youtu.be/-3ekimUWb9I?feature=shared
// https://www.youtube.com/watch?v=eJEpeUH1EMg
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public enum MeshStyle
    {
        sine,
        ripple
    }

    private Mesh mesh;
    private MeshFilter meshFilter;



    [SerializeField] public Vector2 planeSize = new Vector2(1,1);
    [SerializeField] public int planeResolution = 1;
    [SerializeField] public MeshStyle meshStyle;
    [SerializeField] private bool CenterMesh;

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
        if (meshStyle == MeshStyle.sine)
        {
            SineWave(Time.timeSinceLevelLoad);
        } else if (meshStyle == MeshStyle.ripple) 
        {
            Ripple(Time.timeSinceLevelLoad);
        }
        AssignMesh();

        if (GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
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
                if (CenterMesh)
                {
                    vertices.Add(new Vector3(x * xPerStep - planeSize.x / 2, 0, y * yPerStep - planeSize.y / 2));
                } else
                {
                    vertices.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
                }
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

    private void Ripple(float time)
    {
        Vector3 origin = new Vector3(planeSize.x/2, 0, planeSize.y/2);

        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertex = vertices[i];
            float distance = (vertex - origin).magnitude;
            vertex.y = Mathf.Sin(time + distance);
            vertices[i] = vertex;
        }
    }
}
