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
	
	public static bool operator ~(this Vector2 vector, Vector2 vector2)
    {
        
        return true;;
    }
	
	public static bool Approximate(this Vector3 vector, Vector3 vector2, float difDist, out Vector3 dif)
    {
        dif = vector -vector2;
        return (dif.x < difDist && dif.x > -difDist) && (dif.y < difDist && dif.y > -difDist) && (dif.z < difDist && dif.z > -difDist);
    }
}
