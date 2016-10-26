using UnityEngine;
using System.Collections;
using System;

public interface IMovable
{
    void Accelerate();
    void Break();
    void RotateRight();
    void RotateLeft();
}

public interface IBoostable
{
    void BoostSpeed();
    void BoostRotation();
}

public class PlayerMovement : MonoBehaviour, IMovable, IBoostable {

    public float Speed {
        get { return speed; }
    }
    public float RelativeSpeed
    {
        get { return speed / maxSpeed; }
    }
    
    public float acceleration, breaking, maxSpeed, rotationAcceleration, rotationMaxSpeed, startRotation;
    private TrackPosition trackPos;
    private float speed, rotationSpeed;
    private float speedBoost, rotationBoost;
    private bool isAccelerating, isBreaking, isRotatingLeft, isRotatingRight;
    private bool isStuck;

    void Reset()
    {
        breaking = 0.8f;
        acceleration = 0.55f;
        maxSpeed = 0.85f;
        rotationAcceleration = 8f;
        rotationMaxSpeed = 1.8f;
    }

    void Start()
    {
        float factor = GameManager.Instance.settings.speedFactor;
        acceleration *= factor;
        maxSpeed *= factor;
        rotationAcceleration *= factor;
        rotationMaxSpeed *= factor;
        trackPos = Track.Instance.CreateTrackPosition();
        speedBoost = 1f;
        rotationBoost = 1f;
        trackPos.Rotate(startRotation);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Obstacle")
        {
            isStuck = true;
            speed = 0f;
            RemoveBoosts();
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.collider.tag == "Obstacle")
            isStuck = false;
    }
    
    private void UpdateSpeed()
    {
        if (isAccelerating && !isBreaking)
        {
            speed += Time.deltaTime * acceleration * speedBoost;
        } else if (isBreaking && !isAccelerating)
        {
            speed -= Time.deltaTime * breaking * speedBoost;
        } else
        {
            speed -= Time.deltaTime * breaking / 2;
        }
        speed = Mathf.Clamp(speed, 0f, maxSpeed * speedBoost);
        trackPos.Move(speed);
    }

    private void UpdateRotationSpeed()
    {
        if (isRotatingLeft && !isRotatingRight)
        {
            rotationSpeed += Time.deltaTime * rotationAcceleration * rotationBoost;
        }
        else if (isRotatingRight && !isRotatingLeft)
        {
            rotationSpeed -= Time.deltaTime * rotationAcceleration * rotationBoost;
        }
        else
        {
            rotationSpeed *= 0.8f;
        }
        float max = rotationMaxSpeed * rotationBoost;
        rotationSpeed = Mathf.Clamp(rotationSpeed, -max, max);
        trackPos.Rotate(rotationSpeed);
    }

    void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            
            transform.position = trackPos.Position;
            transform.rotation = Quaternion.LookRotation(trackPos.Forward, trackPos.Up);
            return;
        }

        if (!isStuck)
            UpdateSpeed();

        UpdateRotationSpeed();

        isAccelerating = isBreaking = isRotatingLeft = isRotatingRight = false;
        transform.position = trackPos.Position;
        transform.rotation = Quaternion.LookRotation(trackPos.Forward, trackPos.Up);

        // change visuals based on speed
        //var scale = Speed > maxSpeed ? (Speed - maxSpeed) + 1.5f : 1f;
        //transform.localScale = new Vector3(1f, 1f, 1f * scale);
    }
    
	public void Accelerate()
    {
        isAccelerating = true;
    }

    public void Break()
    {
        isBreaking = true;
    }

    public void RotateRight()
    {
        isRotatingRight = true;
    }

    public void RotateLeft()
    {
        isRotatingLeft = true;
    }

    public void BoostSpeed()
    {
        speedBoost = 2f;
        Invoke("RemoveBoosts", 8f);
    }

    public void BoostRotation()
    {
        rotationBoost = 2f;
        Invoke("RemoveBoosts", 8f);
    }

    private void RemoveBoosts()
    {
        speedBoost = 1f;
        rotationBoost = 1f;
    }
}
