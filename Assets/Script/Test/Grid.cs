using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {
    public int xSize;
    public int ySize;

    private Vector3[] vertices;
    private Mesh mesh;

    void Awake()
    {
        StartCoroutine(Generate());
    }

    void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
        if (mesh == null)
        {
            return;
        }
        for (int i = 0; i < mesh.tangents.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(mesh.vertices[i] - (Vector3)mesh.tangents[i] / 2, mesh.vertices[i] + (Vector3)mesh.tangents[i] / 2);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(mesh.vertices[i], mesh.vertices[i] + mesh.normals[i]);
        }
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangent = new Vector4[vertices.Length];
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                float tan = 2 * Mathf.Cos(Mathf.Deg2Rad * 180 / xSize * j) * Mathf.Deg2Rad * 180 / xSize;
                Vector2 t = new Vector2(1, tan);
                t.Normalize();
                vertices[i * (xSize + 1) + j] = new Vector3(j, i, Mathf.Sin(Mathf.Deg2Rad * 180 / xSize * j) * 2);
                uv[i * (xSize + 1) + j] = new Vector2((float)j / xSize, (float)i / ySize);
                tangent[i * (xSize + 1) + j] = new Vector4(t.x, 0, t.y, -1);
            }
        }
        int[] triangles = new int[3 * 2 * xSize * ySize];
        mesh.vertices = vertices;
        mesh.uv = uv;
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                int gridIndex = i * xSize + j;
                triangles[gridIndex * 6 + 0] = i * (xSize + 1) + j + 0;
                triangles[gridIndex * 6 + 4] = triangles[gridIndex * 6 + 1] = (i + 1) * (xSize + 1) + j + 0;
                triangles[gridIndex * 6 + 3] = triangles[gridIndex * 6 + 2] = i  * (xSize + 1) + + j + 1;
                triangles[gridIndex * 6 + 5] = (i + 1) * (xSize + 1) + j + 1;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();
                yield return wait;
            }
        }
        mesh.tangents = tangent;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
