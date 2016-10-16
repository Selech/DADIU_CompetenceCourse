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
    private const int linesPerSegment = 50;

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
        BezierHandle h0, h1;
        for (int i = 0; i < handles.Length-1; i++)
        {
            // calculates <stepsPerSegment> number of points between the two handles
            h0 = handles[i];
            h1 = handles[i+1]; 
            for (int j = 0; j < linesPerSegment; j++)
            {
                // calculate a point along the curve
                list.Add(BezierUtils.GetPoint(h0, h1, (float)j / linesPerSegment));
            }
        }

        // calculate the points from last handle to first in order to make the track a cycle
        h0 = handles[handles.Length - 1];
        h1 = handles[0];
        for (int j = 0; j < linesPerSegment; j++)
        {
            list.Add(BezierUtils.GetPoint(h0, h1, (float)j / linesPerSegment));
        }
        list.Add(list[0]);

        points = list.ToArray();
    }

    public Vector3[] GetPoints()
    {
        return points;
    }
}

