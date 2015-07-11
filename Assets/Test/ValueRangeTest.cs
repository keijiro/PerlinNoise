using UnityEngine;

public class ValueRangeTest : MonoBehaviour
{
    const int iteration = 100 * 100 * 100;

    string DoTest(System.Func<float> generator)
    {
        var sum = 0.0f;
        var min = 0.0f;
        var max = 0.0f;

        for (var i = 0; i < iteration; i++)
        {
            var n = generator.Invoke();
            sum += n / iteration;
            min = Mathf.Min(min, n);
            max = Mathf.Max(max, n);
        }

        return "avg=" + (sum / iteration) + ", min=" + min + ", max=" + max;
    }

    void Start()
    {
        string text = "2D Noise Test: ";

        text += DoTest(() => Perlin.Noise(
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f)));

        text += "\n3D Noise Test: ";

        text += DoTest(() => Perlin.Noise(
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f),
            Random.Range(-100.0f, 100.0f)));

        Debug.Log(text);
    }
}