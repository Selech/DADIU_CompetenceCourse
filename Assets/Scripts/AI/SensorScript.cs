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
    public Transform Left;
    public Transform LeftFine;
    public Transform Center;
    public Transform RightFine;
    public Transform Right;

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
        RaycastHit leftFineHit;
        Debug.DrawRay(LeftFine.position, LeftFine.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out leftFineHit, SensorLength, DetectLayerMask);
        #endregion

        #region Right sensors
        // Right sensor
        ray = new Ray(Right.position, Right.forward);
        RaycastHit rightHit;
        Debug.DrawRay(Right.position, Right.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out rightHit, SensorLength, DetectLayerMask);

        // Right far sensor
        ray = new Ray(RightFine.position, RightFine.forward);
        RaycastHit rightFineHit;
        Debug.DrawRay(RightFine.position, RightFine.forward * SensorLength, Color.green);
        Physics.Raycast(ray, out rightFineHit, SensorLength, DetectLayerMask);
        #endregion

        #region Set turning direction
        // Go forward
        if (!leftHit.collider && !centerHit.collider && !rightHit.collider)
        {
            if (!leftFineHit.collider && rightFineHit.collider)
            {
                parentScript.currentTurnState = AgentBased_AI.State.TurningLeft;
                print("left march!");
            }
            else if (leftFineHit.collider && !rightFineHit.collider)
            {
                parentScript.currentTurnState = AgentBased_AI.State.TurningRight;
                print("right march!");
            }
            else
            {
                parentScript.currentTurnState = AgentBased_AI.State.Forward;
                print("Forward march!");
            }
        }
        // Only hit in the middle
        else if (!leftHit.collider && centerHit.collider && !rightHit.collider)
        {
            if (!leftFineHit.collider && rightFineHit.collider)
            {
                parentScript.currentTurnState = AgentBased_AI.State.TurningLeft;
                print("left march!");
            }
            else 
            {
                parentScript.currentTurnState = AgentBased_AI.State.TurningRight;
                print("right march!");
            }
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

        #endregion

        #region brakingRegion
        if (!centerHit.collider)
        {
            parentScript.currentForwardState = AgentBased_AI.State.Forward;
        }
        else
        {
            parentScript.currentForwardState = centerHit.distance < 10 ? AgentBased_AI.State.Stopping : AgentBased_AI.State.Forward;
        }
        #endregion
    }

}
