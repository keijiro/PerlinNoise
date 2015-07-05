using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    const int iteration = 1000000;

    void Start()
    {
        var sum = 0.0f;
        var min = 0.0f;
        var max = 0.0f;

        for (var i = 0; i < iteration; i++)
        {
            var n = Perlin.Noise(
                Random.Range(-100.0f, 100.0f),
                Random.Range(-100.0f, 100.0f));
            sum += n;
            min = Mathf.Min(min, n);
            max = Mathf.Max(max, n);
        }

        Debug.Log("(2D noise)");
        Debug.Log("avg = " + sum / iteration);
        Debug.Log("min = " + min);
        Debug.Log("max = " + max);

        sum = 0.0f;
        min = 0.0f;
        max = 0.0f;

        for (var i = 0; i < iteration; i++)
        {
            var n = Perlin.Noise(
                Random.Range(-100.0f, 100.0f),
                Random.Range(-100.0f, 100.0f),
                Random.Range(-100.0f, 100.0f));
            sum += n;
            min = Mathf.Min(min, n);
            max = Mathf.Max(max, n);
        }

        Debug.Log("(3D noise)");
        Debug.Log("avg = " + sum / iteration);
        Debug.Log("min = " + min);
        Debug.Log("max = " + max);
    }
}
