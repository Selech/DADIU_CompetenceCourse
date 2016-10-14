using UnityEngine;

public static class BezierUtils
{
    // calculate a point between two bezier handles
    public static Vector3 GetPoint(BezierHandle h0, BezierHandle h1, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * oneMinusT * h0.position +
            3f * oneMinusT * oneMinusT * t * (h0.position + h0.tangent) +
            3f * oneMinusT * t * t * (h1.position - h1.tangent) +
            t * t * t * h1.position;
    }
}