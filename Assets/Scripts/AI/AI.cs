using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class AI : MonoBehaviour
{
    // States
    public enum TurnState
    {
        Left, Right, Straight
    }
    public enum MoveState
    {
        Forward,
        Breaking,
    }

    [HideInInspector]
    public TurnState currentTurnState;
    [HideInInspector]
    public MoveState currentMoveState;
    public SensorScript sensors;

    private PlayerMovement movement;
    private bool lastRightHit;
    private bool lastLeftHit;
    private bool beenStuck;

    public Material mat;
    public Light internalLight;
    public GameObject ball;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        UpdateSensors();
        React();

        float speed = movement.RelativeSpeed;
        // set glow based on speed
        mat.SetFloat("_GlowAmount", speed);
        internalLight.intensity = speed;

        // rescale model based on speed
        var scale = speed > 1f ? speed - 1f + 1.5f : 1f;
        ball.transform.localScale = new Vector3(1f, 1f, scale);
    }

    void UpdateSensors()
    {
        if (movement.Speed == 0)
        {
            lastRightHit = sensors.sensorRightHit || (lastRightHit && ! lastLeftHit) ? true : false;
            lastLeftHit = sensors.sensorLeftHit || (!lastRightHit && lastLeftHit) ? true : false;
        }

        sensors.UpdateSensor();
    }
    void React()
    {
        if (currentMoveState == MoveState.Forward)
            movement.Accelerate();
        else
            movement.Break();    

        if (movement.Speed == 0) // if stuck
        {
            if (currentTurnState == TurnState.Left && !beenStuck)
            {
                movement.RotateLeft();
            }
            else if (currentTurnState == TurnState.Right && !beenStuck)
            {
                movement.RotateRight();
            }
            else
            {
                beenStuck = true;
                if (lastRightHit)
                    movement.RotateRight();
                else if (lastLeftHit)
                     movement.RotateLeft();
            }
        }
        else
        {
            beenStuck = false;
            if (currentTurnState == TurnState.Left)
            {
                movement.RotateLeft();
            }
            else if (currentTurnState == TurnState.Right)
            {
                movement.RotateRight();
            }
        }
    }
}
