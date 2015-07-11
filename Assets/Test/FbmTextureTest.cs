using UnityEngine;

public class FbmTextureTest : MonoBehaviour
{
    public enum TestTarget { Noise2D, Noise3D }
    public TestTarget target;

    int size = 64;
    Texture2D texture;

    void Start()
    {
        texture = new Texture2D(size, size);
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    void UpdateTexture(System.Func<int, int, float> generator)
    {
        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var n = (generator.Invoke(x, y) + 1) / 2;
                texture.SetPixel(x, y, new Color(n, n, n));
            }
        }
        texture.Apply();
    }

    void Update()
    {
        if (target == TestTarget.Noise2D)
            UpdateTexture((x, y) => Perlin.Fbm(1.0f / size * x + Time.time, 1.0f / size * y, 3));
        else
            UpdateTexture((x, y) => Perlin.Fbm(1.0f / size * x, 1.0f / size * y, Time.time, 3));
    }
}