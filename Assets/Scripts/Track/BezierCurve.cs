using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class BezierHandle
{
    public Vector3 position;
    public Vector3 tangent;
}

public class BezierCurve : MonoBehaviour
{
    public bool hideDefaultHandle;
    public BezierHandle[] handles;
    private Vector3[] points;
    private const int maxPointsPerSegment = 100;
    private const float angleThreshold = 0.5f;
    private float totalLength;

    public void Reset()
    {
        hideDefaultHandle = true;
        handles = new BezierHandle[] {
            new BezierHandle()
            {
                position = Vector3.zero,
                tangent = Vector3.forward
            },
            new BezierHandle()
            {
                position = new Vector3(0f, 1f, 5f),
                tangent = Vector3.forward
            },
            new BezierHandle()
            {
                position = new Vector3(-2f, 1f, 5f),
                tangent = Vector3.back,
            },
            new BezierHandle()
            {
                position = new Vector3(-2f, 0, 0),
                tangent = Vector3.back,
            },
        };
        GenerateCurvePoints();
    }

    void OnValidate()
    {
        GenerateCurvePoints();
    }

    // generates the curve of a lot of points
    public void GenerateCurvePoints()
    {
        List<Vector3> list = new List<Vector3>();
        Vector3 oldPoint = handles[0].position;
        Vector3 oldTangent = handles[0].tangent;
        Vector3 newPoint, newTangent;
        BezierHandle h0, h1;
        totalLength = 0;

        for (int i = 0; i < handles.Length; i++)
        {
            // calculates <stepsPerSegment> number of points between the two handles
            h0 = handles[i];
            h1 = handles[(i+1) % handles.Length]; 
            for (int j = 1; j <= maxPointsPerSegment; j++)
            {
                // calculate the point and tangent on the curve
                newPoint = BezierUtils.GetPoint(h0, h1, (float)j / maxPointsPerSegment);
                newTangent = (newPoint - oldPoint).normalized;
                // check if angle between tangents exceeds threshold
                if (Vector3.Angle(oldTangent, newTangent) >= angleThreshold)
                {
                    // add point and proceed from there
                    totalLength += (newPoint - oldPoint).magnitude;
                    list.Add(newPoint);
                    oldPoint = newPoint;
                    oldTangent = newTangent;
                }
            }
        }
        
        points = list.ToArray();
    }

    public Vector3[] GetPoints()
    {
        return points;
    }
}

