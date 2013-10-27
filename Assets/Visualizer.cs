using UnityEngine;
using System.Collections;

public class Visualizer : MonoBehaviour
{
    public int division = 32;
    public int dimension = 3;
    public int octave = 3;
    public float amplitude = 0.2f;
    public float speed = 1.0f;
    public float scale = 4.0f;
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

        if (dimension == 1) {
            for (var i = 0; i < vertices.Length; i++) {
                var v = vertices [i];
                var nc = v.x * scale + Time.time * speed;
                vertices [i].y = Perlin.Fbm (nc, octave) * amplitude;
            }
        } else if (dimension == 2) {
            for (var i = 0; i < vertices.Length; i++) {
                var v = vertices [i];
                var nc = new Vector2 (v.x * scale, v.z * scale) + Vector2.one * Time.time * speed;
                vertices [i].y = Perlin.Fbm (nc, octave) * amplitude;
            }
        } else {
            for (var i = 0; i < vertices.Length; i++) {
                var v = vertices [i];
                var nc = new Vector3 (v.x * scale, Time.time * speed, v.z * scale);
                vertices [i].y = Perlin.Fbm (nc, octave) * amplitude;
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals ();
    }
}