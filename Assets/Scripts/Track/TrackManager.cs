using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BezierCurve))]
public class TrackManager : MonoBehaviour {
    private Vector3[] points;

	// Use this for initialization
	void Start () {
        points = GetComponent<BezierCurve>().GetPointsInWorldSpace();



    }
	
	// Update is called once per frame
	void Update () {
	    

	}
}
