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

    public void Reset()
    {
        hideDefaultHandle = true;
        handles = new BezierHandle[] {
            new BezierHandle()
            {
                position = Vector3.zero,
                tangent = Vector3.right
            },
            new BezierHandle()
            {
                position = new Vector3(1, 1, 0),
                tangent = Vector3.right,
            },
            new BezierHandle()
            {
                position = new Vector3(3, 2, 0),
                tangent = Vector3.right,
            },
        };
        CalculatePoints();
    }

    void OnValidate()
    {
        CalculatePoints();
    }

    public void CalculatePoints()
    {
        GenerateCurvePoints(5);
    }

    // generates the curve of a lot of points
    public void GenerateCurvePoints(int stepsPerSegment)
    {
        List<Vector3> list = new List<Vector3>();
        BezierHandle h0, h1;
        for (int i = 0; i < handles.Length-1; i++)
        {
            // calculates <stepsPerSegment> number of points between the two handles
            h0 = handles[i];
            h1 = handles[i+1]; 
            for (int j = 0; j < stepsPerSegment; j++)
            {
                // calculate a point along the curve
                list.Add(BezierUtils.GetPoint(h0, h1, (float)j / stepsPerSegment));
            }
        }

        // calculate the points from last handle to first in order to make the track a cycle
        h0 = handles[handles.Length - 1];
        h1 = handles[0];
        for (int j = 0; j < stepsPerSegment; j++)
        {
            list.Add(BezierUtils.GetPoint(h0, h1, (float)j / stepsPerSegment));
        }
        list.Add(list[0]);

        points = list.ToArray();
    }

    public Vector3[] GetPoints()
    {
        return points;
    }

    public Vector3[] GetPointsInWorldSpace()
    {
        return points.Select(p => transform.TransformPoint(p)).ToArray();
    }
}

