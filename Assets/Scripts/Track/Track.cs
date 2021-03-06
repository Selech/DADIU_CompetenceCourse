﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(BezierCurve))]
public class Track : MonoBehaviour {
    public static Track Instance { get; private set; } 

    public const float radius = 0.65f;
    private const int verticesPerPoint = 10;
    private Vector3[] points;
    private float trackLength;

	// Use this for initialization
	void Awake () {
        
        if (Instance == null)
        {
            Instance = this;}
            
        else
        {
            Destroy(gameObject);
            return;
        }

        // transform the curve points to world space
        points = GetComponent<BezierCurve>().GetPoints();
        for (int i = 0; i < points.Length; i++)
        {
            trackLength += (points[(i + 1) % points.Length] - points[i]).magnitude;
        }
        GenerateTrackMesh();
    }

    public TrackPosition CreateTrackPosition()
    {
        return new TrackPosition(points, trackLength);
    }

    private void GenerateTrackMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "TrackMesh";
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private Vector3[] GenerateVertices()
    {
        List<Vector3> vertices = new List<Vector3>();
        Quaternion rotation;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 dir = (points[(i+1)%points.Length] - points[i]).normalized;
            for (int j = 0; j < verticesPerPoint; j++)
            {
                rotation = Quaternion.AngleAxis((360f * j) / verticesPerPoint, dir);
                vertices.Add(points[i] + rotation * new Vector3(0f, radius, 0f));
            }
        }
        
        return vertices.ToArray();
    }

    private int[] GenerateTriangles()
    {
        List<int> triangles = new List<int>();
        for(int i = 0; i < points.Length; i++)
        {
            Connect(i * verticesPerPoint, ((i+1) % points.Length) * verticesPerPoint, triangles);
        }

        return triangles.ToArray();
    }

    private void Connect(int i0, int i1, List<int> triangles)
    {
        for (int i = 0; i < verticesPerPoint; i++)
        {
            int vi = (i + 1) % verticesPerPoint;
            // triangle one
            triangles.Add(i0 + i);
            triangles.Add(i0 + vi);
            triangles.Add(i1 + i);
            // triangle two
            triangles.Add(i0 + vi);
            triangles.Add(i1 + vi);
            triangles.Add(i1 + i);
        }
    }


}
