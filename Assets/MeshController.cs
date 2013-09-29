using UnityEngine;
using System.Collections;

public class MeshController : MonoBehaviour
{
    public int division = 32;
    MeshFilter meshFilter;

    void Awake ()
    {
        meshFilter = GetComponent<MeshFilter> ();

        Vector3[] vertices = new Vector3[division * division];
        var i = 0;
        for (var row = 0; row < division; row++) {
            for (var col = 0; col < division; col++) {
                vertices [i++] = new Vector3 (1.0f * col / division - 0.5f, 0.0f, 1.0f * row / division - 0.5f);
            }
        }

        int[] indices = new int[2 * 3 * (division - 1) * (division - 1)];
        i = 0;
        for (var row = 0; row < division - 1; row++) {
            for (var col = 0; col < division - 1; col++) {
                var offs = row * division + col;
                indices [i++] = offs;
                indices [i++] = offs + division;
                indices [i++] = offs + division + 1;
                indices [i++] = offs;
                indices [i++] = offs + division + 1;
                indices [i++] = offs + 1;
            }
        }

        var mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.SetTriangles (indices, 0);
        mesh.Optimize ();
        mesh.RecalculateNormals ();

        meshFilter.sharedMesh = mesh;
    }

    void Update ()
    {
        var mesh = meshFilter.sharedMesh;
        var vertices = mesh.vertices;
        for (var i = 0; i < vertices.Length; i++) {
            var v = vertices [i];
            var nc = new Vector3 (v.x * 4, Time.time, v.z * 4);
            vertices [i].y = Perlin.Fbm (nc, 3) * 0.2f;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals ();
    }
}