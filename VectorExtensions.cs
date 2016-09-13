using UnityEngine;
using System.Collections;

public static class VectorExtensions
{
    public static Vector2 Rotate(this Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float x = cos * vector.x - sin * vector.y;
        float y = sin * vector.x + cos * vector.y;
        return new Vector2(x, y);
    }
}
