using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float slerpAmount;

	// Use this for initialization
	void Reset () {
        target = GameObject.Find("Player").transform;
        slerpAmount = 10f;
        offset = new Vector3(0, -3, 10);
	}

	void Start()
	{
        transform.parent.position = target.position;
        transform.localPosition = transform.parent.up * offset.y + transform.parent.forward * offset.z;
        transform.rotation = target.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position - target.forward * offset.z - target.up * offset.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * slerpAmount);
	}
}
