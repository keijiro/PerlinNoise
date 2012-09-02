#pragma strict

@Range(0, 5)
var testType = 5;

private var resolution = 64;
private var mesh : Mesh;

function Awake() {
    mesh = new Mesh();
    GetComponent.<MeshFilter>().mesh = mesh;

    var vertices = new Vector3[resolution * resolution];
    var indices = new int[resolution * resolution];
    var offs = 0;
    for (var v = 0; v < resolution; v++) {
        for (var u = 0; u < resolution; u++) {
            vertices[offs] = Vector3(
                2.0 / resolution * u - 1.0,
                0.0,
                2.0 / resolution * v - 1.0
            );
            if (v & 1) {
                indices[offs] = offs + resolution - u * 2 - 1;
            } else {
                indices[offs] = offs;
            }
            offs++;
        }
    }

    mesh.vertices = vertices;
    mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
    mesh.RecalculateBounds();
}

function Update() {
    var vertices = mesh.vertices;

    var offs = 0;
    for (var v = 0; v < resolution; v++) {
        for (var u = 0; u < resolution; u++) {
            var x = 2.0 / resolution * u;
            var y : float;
            var z = 2.0 / resolution * v;
            var t = Time.time * 0.2;

            if (testType == 0) {
                y = Perlin.Noise(x + t);
            } else if (testType == 1) {
                y = Perlin.Fbm(x + t, 4);
            } else if (testType == 2) {
                y = Perlin.Noise(Vector2(x + t, z + t));
            } else if (testType == 3) {
                y = Perlin.Fbm(Vector2(x + t, z + t), 4);
            } else if (testType == 4) {
                y = Perlin.Noise(Vector3(x, t, z));
            } else {
                y = Perlin.Fbm(Vector3(x, t, z), 4);
            }
            
            vertices[offs].y = y;
            offs++;
        }
    }

    mesh.vertices = vertices;
}
