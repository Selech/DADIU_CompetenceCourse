using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float maxSpeed;
	public float rotateSpeed;
    private float speed;
    private TrackPosition trackPosition;
    public Material mat;
	
    void Reset()
    {
        maxSpeed = 0.45f;
        rotateSpeed = 2f;
    }

    void CalculateMovement()
    {

    }

	// Update is called once per frame
	void Update () {

        // move player
        float dist = Input.GetAxis("Vertical") * maxSpeed;
        trackPosition.Move(dist);
        // set glow based on speed
        mat.SetFloat("_GlowPower", 5 - (dist * 15));

        // rotate player
        var degrees = -Input.GetAxis("Horizontal") * rotateSpeed;
        trackPosition.Rotate(degrees);

		transform.position = trackPosition.Position;
        transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);
	}

    // called from the PlayerCollision
    public void OnCollisionEnter()
    {
        maxSpeed = 0;
    }

    // called from the PlayerCollision
    public void OnCollisionExit()
    {
        maxSpeed = 1f;
    }

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
        transform.forward = trackPosition.Forward;
    }
}
