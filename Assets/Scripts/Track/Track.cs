using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(BezierCurve))]
public class Track : MonoBehaviour {
    public const float radius = 0.5f;
    private const int verticesPerPoint = 4;
    

    private Vector3[] points;




	// Use this for initialization
	void Awake () {
        // transform the curve points to world space
        points = GetComponent<BezierCurve>().GetPoints();
    }

    public TrackPosition CreateTrackPosition()
    {
        return new TrackPosition(points);
    }

    private void GenerateTrackMesh()
    {

    }

    private void Connect(Vector3 p0, Vector3 p1)
    {

    }
}
