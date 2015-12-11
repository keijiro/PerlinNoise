using UnityEngine;

public class FbmTextureTest : MonoBehaviour
{
    public enum TestTarget {
        Noise1D, Noise2D, Noise3D
    }

    [SerializeField]
    TestTarget _target;

    [SerializeField, Range(1, 5)]
    int _fractalLevel = 1;

    int size = 64;
    Texture2D texture;

    void Start()
    {
        texture = new Texture2D(size, size);
        texture.wrapMode = TextureWrapMode.Clamp;
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    void UpdateTexture(System.Func<float, float, float, float> generator)
    {
        var scale = 1.0f / size;
        var time = Time.time;

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var n = generator.Invoke(x * scale, y * scale, time);
                texture.SetPixel(x, y, Color.white * (n / 1.4f + 0.5f));
            }
        }

        texture.Apply();
    }

    void Update()
    {
        if (_target == TestTarget.Noise1D)
            UpdateTexture((x, y, t) => Perlin.Fbm(x + t, _fractalLevel));
        else if (_target == TestTarget.Noise2D)
            UpdateTexture((x, y, t) => Perlin.Fbm(x + t, y, _fractalLevel));
        else
            UpdateTexture((x, y, t) => Perlin.Fbm(x, y, t, _fractalLevel));
    }
}
