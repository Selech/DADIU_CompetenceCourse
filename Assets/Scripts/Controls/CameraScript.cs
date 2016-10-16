using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

public Transform parent;
	public Transform target;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		this.transform.localPosition = target.position - new Vector3(0, offset.y * target.up.y, offset.z * target.forward.z) ;
		this.transform.LookAt(target.position);
	}

	void OnValidate()
	{
		Start();
		Update();
	}
	
	// Update is called once per frame
	void Update () {
		parent.transform.position = target.position;
		parent.transform.rotation = target.rotation;// Quaternion.Euler(0,0,target.rotation.eulerAngles.z);
	}
}
