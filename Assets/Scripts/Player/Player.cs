using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float acceleration;
    public float breaking;
    public float maxSpeed;
	public float rotateSpeed;
    private float velocity;
    private bool isStuck;
    private TrackPosition trackPosition;
    public Material mat;
    public Light internalLight;
	
    void Reset()
    {
        acceleration = 1f;
        breaking = 0.25f;
        maxSpeed = 1f;
        rotateSpeed = 2f;
    }

    void Move()
    {
        // velocity change
        float dv = Input.GetAxisRaw("Vertical") * Time.deltaTime * acceleration;
        if (dv == 0)
            velocity -= breaking * Time.deltaTime;
        else
            velocity += dv;
        velocity = Mathf.Clamp(velocity, 0f, maxSpeed);
        trackPosition.Move(velocity);

        
    }

	// Update is called once per frame
	void Update () {

        if (!isStuck)
            Move();

        // set glow based on velocity
        float percent = velocity / maxSpeed;
        mat.SetFloat("_GlowPower", 2 - (2 * percent));
	    internalLight.intensity = 1*percent;

        // rotate player
        var degrees = -Input.GetAxis("Horizontal") * rotateSpeed;
        trackPosition.Rotate(degrees);

		transform.position = trackPosition.Position;
        transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);
	}

    // called from the PlayerCollision
    public void OnCollisionEnter()
    {
        isStuck = true;
        velocity = 0f;
    }

    // called from the PlayerCollision
    public void OnCollisionExit()
    {
        isStuck = false;
    }

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
        transform.forward = trackPosition.Forward;
    }

    public void ActivatePowerup()
    {
        // TODO
        print("POWERUP!");
    }
}
