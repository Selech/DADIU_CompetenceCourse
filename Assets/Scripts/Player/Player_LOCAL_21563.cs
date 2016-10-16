using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    private Vector3 position = Vector3.zero; 
	public float forwardSpeed;
	public float rotatationSpeed;
	public float correction;

	private float rotatation = 0.0f;
    private TrackPosition trackPosition;

	public GameObject parent;
	
    void Reset()
    {
        speed = 0.4f;
    }

	// Update is called once per frame
	void FixedUpdate () {
        trackPosition.Move(speed); 
        //transform.position = trackPosition.Position;

        SetRotation();
		SetPosition();

		
		var targetDirection = Quaternion.Euler(Quaternion.LookRotation(trackPosition.Forward).eulerAngles + Quaternion.AngleAxis(rotatation,trackPosition.Forward).eulerAngles);
		print (trackPosition.Forward);
		print (targetDirection.eulerAngles);

		parent.transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection,  0.05f);
		//this.transform.LookAt(trackPosition.Position + trackPosition.Forward);
	}

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
    }

	private void SetRotation(){
		var tempRotation = rotatation;
		// Get inputs
		if(Input.GetKey(KeyCode.LeftArrow)){
			 tempRotation += rotatationSpeed;
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			tempRotation += -rotatationSpeed;
		}
		
		// Calculate the rotation
		if(tempRotation >= 0.0f && tempRotation <= 360.0f){ 
			rotatation = tempRotation;
		}
		else if(tempRotation < 0){
			rotatation = 360 - tempRotation;
		}
		else if(tempRotation > 360){
			rotatation =  tempRotation - 360;
		}

		
	}

	private void SetPosition(){
		position = trackPosition.Position - new Vector3(0,0.5f,0);
		var r = 0.75f;

		var x = r * Mathf.Cos((rotatation+correction) * (Mathf.PI / 180)) ;
		var y = r * Mathf.Sin((rotatation+correction) * (Mathf.PI / 180)) ;
		//var z = r* Mathf.Cos((rotatation+correction) * (Mathf.PI / 180)) ;

		var finalPosition = new Vector3(position.x+x,position.y+y,position.z);

		this.transform.position = finalPosition;
	}

}
