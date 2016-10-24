using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float slerpAmount;

    public ParticleSystem ParticleSystem;

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

	    var shakeAmount = (target.GetComponent<Player>().velocity - 0.6f) > 0 ? (target.GetComponent<Player>().velocity - 0.6f) : 0f;

	    ParticleSystem.startSize = 0.02f*shakeAmount;
	    // ParticleSystem.startSpeed = 10f*shakeAmount;

        var shakeVector = shakeAmount * new Vector3(Random.Range(0.05f, 0.15f), Random.Range(0.05f, 0.15f));
	    transform.position += shakeVector;
	}

}
