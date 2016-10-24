using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float acceleration;
    private float _acceleration;
    public float breaking;
    public float maxSpeed;
    private float _maxSpeed;
    public float rotateSpeed;
    public float velocity;
    private bool isStuck;
    private bool powerUpActivated;
    private TrackPosition trackPosition;
    public Material mat;
    public Light internalLight;

    void Start()
    {
        _maxSpeed = maxSpeed;
        _acceleration = acceleration;
    }

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
        float dv = Input.GetAxisRaw("Vertical") * Time.deltaTime * _acceleration;
        if (dv == 0)
            velocity -= breaking * Time.deltaTime;
        else
            velocity += dv;
        velocity = Mathf.Clamp(velocity, 0f, _maxSpeed);
        trackPosition.Move(velocity);

        
    }

	// Update is called once per frame
	void Update ()
	{
        if (!isStuck)
            Move();

        // set glow based on velocity
        float percent = velocity / _maxSpeed;
        mat.SetFloat("_GlowPower", 2 - (2 * percent));
	    internalLight.intensity = 1*percent;

        // rotate player
        var degrees = -Input.GetAxis("Horizontal") * rotateSpeed;
        trackPosition.Rotate(degrees);

		transform.position = trackPosition.Position;
        transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);

        var scale = (velocity - maxSpeed) > 0f ? (velocity - maxSpeed) + 1.5f : 1f;
        transform.localScale = new Vector3(1f, 1f, 1f * scale);
    }

    // called from the PlayerCollision
    public void CollisionEnter()
    {
        isStuck = true;
        velocity = 0f;
    }

    // called from the PlayerCollision
    public void CollisionExit()
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
        Camera.main.GetComponent<CameraScript>().TriggerShake();
        print("POWERUP!");
        StartCoroutine(Powerup());
    }

    IEnumerator Powerup()
    {
        powerUpActivated = true;
        _maxSpeed = maxSpeed*2;
        _acceleration = acceleration * 2;
        yield return new WaitForSeconds(5f);
        powerUpActivated = false;
        _maxSpeed = maxSpeed;
        _acceleration = acceleration;
    }
}
