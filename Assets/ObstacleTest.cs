using UnityEngine;
using System.Collections;

public class ObstacleTest : MonoBehaviour {

	void OnCollisionEnter(Collision other)
	{
		other.gameObject.GetComponentInParent<Player>().speed = 0.0f;
	}

	void OnCollisionExit(Collision other)
	{
		other.gameObject.GetComponentInParent<Player>().speed = 0.2f;
	}
	
}
