using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float acceleration;
    private float _acceleration;
    public float breaking;
    public float maxSpeed;
    private float _maxSpeed;
    public float rotateSpeed;
    public float _rotateSpeed;
    public float velocity;
    private bool isStuck;
    private TrackPosition trackPosition;
    public Material mat;
    public Light internalLight;

    public AudioSource[] audioSources;

    void Start()
    {
        maxSpeed *= GameManager.Instance.settings.speedFactor;
        _maxSpeed = maxSpeed;
        acceleration *= GameManager.Instance.settings.speedFactor;
        _acceleration = acceleration;

        rotateSpeed *= GameManager.Instance.settings.speedFactor;
        _rotateSpeed = rotateSpeed;

        audioSources = this.GetComponents<AudioSource>();
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
        mat.SetFloat("_GlowAmount", 1f * percent);
	    internalLight.intensity = 1f*percent;

        // pitch roll sound
	    audioSources[0].pitch = 0.8f*percent + 0.3f;
        audioSources[0].volume = 0.8f * percent + 0.1f;

        // rotate player
        var degrees = -Input.GetAxis("Horizontal") * _rotateSpeed;
        trackPosition.Rotate(degrees);

		transform.position = trackPosition.Position;
        transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);

        var scale = (velocity - maxSpeed) > 0f ? (velocity - maxSpeed) + 1.5f : 1f;
        transform.localScale = new Vector3(1f, 1f, 1f * scale);
    }

    // called from the PlayerCollision
    public void CollisionEnter()
    {
        audioSources[1].Play();
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

    public void ActivateRotationPowerup()
    {
        audioSources[2].Play();
        StartCoroutine(PowerupRotation());
    }

    IEnumerator PowerupRotation()
    {
        _rotateSpeed = rotateSpeed * 2;
        yield return new WaitForSeconds(5f);
        _rotateSpeed = rotateSpeed;
    }

    public void ActivateSpeedPowerup()
    {
        audioSources[2].Play();
        StartCoroutine(PowerupSpeed());
    }

    IEnumerator PowerupSpeed()
    {
        _maxSpeed = maxSpeed*2;
        _acceleration = acceleration * 2;
        yield return new WaitForSeconds(5f);
        _maxSpeed = maxSpeed;
        _acceleration = acceleration;
    }
}
