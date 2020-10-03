using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector2 EvaluateQuadratic(Vector2 a, Vector2 b, Vector2 c, float time)
    {
        Vector2 p0 = Vector2.Lerp(a, b, time);
        Vector2 p1 = Vector2.Lerp(b, c, time);
        return Vector2.Lerp(p0, p1, time);
    }

    public static Vector2 EvaluateCubic(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float time)
    {
        Vector2 p0 = EvaluateQuadratic(a, b, c, time);
        Vector2 p1 = EvaluateQuadratic(b, c, d, time);
        return Vector2.Lerp(p0, p1, time);
    }
}
