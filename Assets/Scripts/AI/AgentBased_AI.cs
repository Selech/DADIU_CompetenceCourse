using UnityEngine;
using System.Collections;

public class AgentBased_AI : MonoBehaviour
{
    // States
    public enum State
    {
        Forward,
        Stopping,
        TurningRight,
        TurningLeft
    }

    public State currentTurnState;
    public State currentForwardState;

    private float _acceleration;
    public float acceleration;
    public float breaking;
    private float _maxSpeed;
    public float maxSpeed;
    public float rotateSpeed;
    private float _rotateSpeed;
    private float velocity;
    private bool isStuck;
    private TrackPosition trackPosition;
    public Material mat;
    public Light internalLight;

    public SensorScript sensors;

    private bool lastRightHit;
    private bool lastLeftHit;

    private bool beenStuck;

    void Start()
    {
        maxSpeed *= GameManager.singleton.settings.speedFactor;
        _maxSpeed = maxSpeed;
        acceleration *= GameManager.singleton.settings.speedFactor;
        _acceleration = acceleration;
		rotateSpeed *= GameManager.singleton.settings.speedFactor;
        _rotateSpeed = rotateSpeed;
        
    }

    void Awake()
    {
        //Time.timeScale = 10.5f;
    }

    void Reset()
    {
        acceleration = 1f;
        breaking = 0.25f;
        maxSpeed = 1f;
        rotateSpeed = 2f;
    }


    void Update()
    {
        UpdateSensors();
        React();
    }

    void UpdateSensors()
    {
        if (isStuck)
        {
            lastRightHit = sensors.sensorRightHit ? true : false;
            lastLeftHit = sensors.sensorLeftHit ? true : false;
        }

        sensors.UpdateSensor();
    }

    void Move()
    {
        // velocity change
        float dv = 1f * Time.deltaTime * _acceleration;
        if (currentForwardState == State.Stopping)
            velocity -= breaking * Time.deltaTime;
        else
            velocity += dv;
        velocity = Mathf.Clamp(velocity, 0f, _maxSpeed);
        trackPosition.Move(velocity);
    }

    // Update is called once per frame
    void React()
    {
        if (isStuck)
        {
            mat.SetFloat("_GlowAmount", 0);
            internalLight.intensity = 1 * 0f;

            // rotate player
            float degrees = 0f;
            if (currentTurnState == State.TurningLeft && !beenStuck)
            {
                degrees = 1 * rotateSpeed;
            }
            else if (currentTurnState == State.TurningRight && !beenStuck)
            {
                degrees = -1 * rotateSpeed;
            }
            else
            {
                beenStuck = true;
                degrees = -1 * rotateSpeed;
            }
            trackPosition.Rotate(degrees);

            transform.position = trackPosition.Position;
            transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);
        }
        else
        {
            Move();
            beenStuck = false;
            // set glow based on velocity
            float percent = velocity / _maxSpeed;
            mat.SetFloat("_GlowAmount", 1f * percent);
            internalLight.intensity = 1f * percent;

            // rotate player
            float degrees = 0f;
            if (currentTurnState == State.TurningLeft)
            {
                degrees = 1 * _rotateSpeed;
            }
            else if (currentTurnState == State.TurningRight)
            {
                degrees = -1 * _rotateSpeed;
            }
            trackPosition.Rotate(degrees);

            transform.position = trackPosition.Position;
            transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);

        }
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

    public void ActivateRotationPowerup()
    {
        //Camera.main.GetComponent<CameraScript>().TriggerShake();
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
        Camera.main.GetComponent<CameraScript>().TriggerShake();
        StartCoroutine(PowerupSpeed());
    }

    IEnumerator PowerupSpeed()
    {
        _maxSpeed = maxSpeed * 2;
        _acceleration = acceleration * 2;
        yield return new WaitForSeconds(5f);
        _maxSpeed = maxSpeed;
        _acceleration = acceleration;
    }
}
