using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SensorScript : MonoBehaviour {

    public AgentBased_AI parentScript;

    [HideInInspector]
    public bool sensorCenterHit;
    [HideInInspector]
    public bool sensorRightHit;
    [HideInInspector]
    public bool sensorLeftHit;

    [Header("Sensors")]
    public Transform LeftFar;
    public Transform Left;
    public Transform Center;
    public Transform Right;
    public Transform RightFar;

    [Header("Sensors setting")]
    public float SensorLength;
    public LayerMask DetectLayerMask;

	public void UpdateSensor () {
        sensorCenterHit = false;
        sensorRightHit = false;
        sensorLeftHit = false;

        SingleHit();
    }

    private void SingleHit()
    {
        #region Center sensor
        // Center sensor
        Ray ray = new Ray(Center.position, Center.forward);
        RaycastHit centerHit;
        Debug.DrawRay(Center.transform.position, Center.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out centerHit, SensorLength, DetectLayerMask);
        #endregion

        #region Left sensors
        // Left sensor
        ray = new Ray(Left.position, Left.forward);
        RaycastHit leftHit;
        Debug.DrawRay(Left.position, Left.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out leftHit, SensorLength, DetectLayerMask);

        // Left far sensor
        ray = new Ray(Left.position, Left.forward);
        RaycastHit leftFarHit;
        Debug.DrawRay(LeftFar.position, LeftFar.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out leftFarHit, SensorLength, DetectLayerMask);
        #endregion

        #region Right sensors
        // Right sensor
        ray = new Ray(Right.position, Right.forward);
        RaycastHit rightHit;
        Debug.DrawRay(Right.position, Right.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out rightHit, SensorLength, DetectLayerMask);

        // Right far sensor
        ray = new Ray(RightFar.position, RightFar.forward);
        RaycastHit rightFarHit;
        Debug.DrawRay(RightFar.position, RightFar.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out rightFarHit, SensorLength, DetectLayerMask);
        #endregion

        // Go forward
        if (!leftHit.collider && !centerHit.collider && !rightHit.collider)
        {
            parentScript.currentTurnState = AgentBased_AI.State.Forward;
        }
        // Go turn left
        else if (!leftHit.collider && rightHit.collider)
        {
            parentScript.currentTurnState = AgentBased_AI.State.TurningLeft;
        }
        // Go turn right
        else if (leftHit.collider && !rightHit.collider)
        {
            parentScript.currentTurnState = AgentBased_AI.State.TurningRight;
        }
        else if (leftHit.collider && rightHit.collider)
        {
            parentScript.currentTurnState = leftHit.distance > rightHit.distance
                ? AgentBased_AI.State.TurningLeft : AgentBased_AI.State.TurningRight;
        }
        else
        {
            parentScript.currentTurnState = AgentBased_AI.State.Forward;
        }

        if (!centerHit.collider)
        {
            parentScript.currentForwardState = AgentBased_AI.State.Forward;
        }
        else
        {
            parentScript.currentForwardState = centerHit.distance < 10 ? AgentBased_AI.State.Stopping : AgentBased_AI.State.Forward;
        }
    }

    private void SetState()
    {
        /*if (rightHit.distance + rightFarHit.distance < leftHit.distance + leftFarHit.distance)
        {
            parentScript.currentState = AgentBased_AI.State.TurningLeft;
        }
        else if (rightHit.distance + rightFarHit.distance > leftHit.distance + leftFarHit.distance)
        {
            parentScript.currentState = AgentBased_AI.State.TurningRight;
        }
        else
        {
            parentScript.currentState = AgentBased_AI.State.Forward;
        }*/
    }

}
