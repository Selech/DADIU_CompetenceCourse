using UnityEngine;
using System.Collections;

public class SpinnerScript : MonoBehaviour
{

    public float RotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    this.transform.Rotate(Vector3.forward, RotationSpeed);
	}
}
