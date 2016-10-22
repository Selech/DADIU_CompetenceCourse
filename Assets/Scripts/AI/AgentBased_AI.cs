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

    public float acceleration;
    public float breaking;
    public float maxSpeed;
    public float rotateSpeed;
    private float velocity;
    private bool isStuck;
    private TrackPosition trackPosition;
    public Material mat;
    public Light internalLight;

    public SensorScript sensors;

    private bool lastRightHit;
    private bool lastLeftHit;

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
        float dv = 1f * Time.deltaTime * acceleration;
        if (currentForwardState == State.Stopping)
            velocity -= breaking * Time.deltaTime;
        else
            velocity += dv;
        velocity = Mathf.Clamp(velocity, 0f, maxSpeed);
        trackPosition.Move(velocity);
    }

    // Update is called once per frame
    void React()
    {
        if (isStuck)
        {
            mat.SetFloat("_GlowPower", 2 - (2 * 0f));
            internalLight.intensity = 1 * 0f;

            // rotate player
            float degrees = 0f;
            if (currentTurnState == State.TurningLeft)
            {
                degrees = 1 * rotateSpeed;
            }
            else if (currentTurnState == State.TurningRight)
            {
                degrees = -1 * rotateSpeed;
            }
            trackPosition.Rotate(degrees);

            transform.position = trackPosition.Position;
            transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);
        }
        else
        {
            Move();

            // set glow based on velocity
            float percent = velocity / maxSpeed;
            mat.SetFloat("_GlowPower", 2 - (2 * percent));
            internalLight.intensity = 1 * percent;

            // rotate player
            float degrees = 0f;
            if (currentTurnState == State.TurningLeft)
            {
                degrees = 2 * rotateSpeed;
            }
            else if (currentTurnState == State.TurningRight)
            {
                degrees = -2*rotateSpeed;
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
}
