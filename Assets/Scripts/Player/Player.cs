using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float maxSpeed;
	public float rotateSpeed;
    private float speed;
    private TrackPosition trackPosition;
	
    void Reset()
    {
        maxSpeed = 0.45f;
        rotateSpeed = 2f;
    }

	// Update is called once per frame
	void Update () {

        // move player
        float dist = Input.GetAxis("Vertical") * maxSpeed;
        trackPosition.Move(dist);
        // rotate player
        var degrees = -Input.GetAxis("Horizontal") * rotateSpeed;
        trackPosition.Rotate(degrees);

		transform.position = trackPosition.Position;
        transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);
	}

    void OnCollisionEnter(Collision other)
    {
        print("Collide");
    }

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
        transform.forward = trackPosition.Forward;
    }
}
