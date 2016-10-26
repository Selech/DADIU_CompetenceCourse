using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 0.25f);
	}
}
