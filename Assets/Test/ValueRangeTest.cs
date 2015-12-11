using UnityEngine;

public class ValueRangeTest : MonoBehaviour
{
    [SerializeField]
    int _iteration = 100 * 100;

    string DoTest(System.Func<float> generator)
    {
        var sum = 0.0f;
        var min = 0.0f;
        var max = 0.0f;

        for (var i = 0; i < _iteration; i++)
        {
            var n = generator.Invoke();
            sum += n / _iteration;
            min = Mathf.Min(min, n);
            max = Mathf.Max(max, n);
        }

        return "avg=" + (sum / _iteration) + ", min=" + min + ", max=" + max;
    }

    void Start()
    {
        string text = "1D Noise Test: ";

        text += DoTest(() => Perlin.Noise(
            Random.Range(-100.0f, 100.0f)));

        text += "\n2D Noise Test: ";

        text += DoTest(() => Perlin.Noise(
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f)));

        text += "\n3D Noise Test: ";

        text += DoTest(() => Perlin.Noise(
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f)));

        text += "\n1D fBm Test: ";

        text += DoTest(() => Perlin.Fbm(
            Random.Range(-100.0f, 100.0f), 5));

        text += "\n2D fBm Test: ";

        text += DoTest(() => Perlin.Fbm(
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f), 5));

        text += "\n3D fBm Test: ";

        text += DoTest(() => Perlin.Fbm(
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f), 5));

        Debug.Log(text);
    }
}
