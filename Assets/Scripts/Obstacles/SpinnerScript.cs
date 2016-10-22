using UnityEngine;
using System.Collections;

public class SpinnerScript : MonoBehaviour
{
    public enum Vectors
    {
        Xaxis = 0,
        Yaxis = 1,
        Zaxis = 2
    }

    public Vectors Direction;
    public float RotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var rotationAxis = Vector3.forward;
	    switch (Direction)
	    {
            case Vectors.Zaxis:
                rotationAxis = Vector3.forward;
	            break;

            case Vectors.Yaxis:
                rotationAxis = Vector3.up;
                break;

            case Vectors.Xaxis:
                rotationAxis = Vector3.right;
                break;

            default:break;
	    }
	    this.transform.Rotate(rotationAxis, RotationSpeed);
	}
}
