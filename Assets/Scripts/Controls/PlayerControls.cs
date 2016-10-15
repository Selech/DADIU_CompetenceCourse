using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	private Vector3 position = Vector3.zero; 
	public float forwardSpeed;
	public float rotatationSpeed;
	public float correction;

	private float rotatation = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SetRotation();
		SetPosition();
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

		this.transform.rotation = Quaternion.AngleAxis(rotatation,Vector3.forward);
	}

	private void SetPosition(){
		position += new Vector3(0,0,forwardSpeed);

		var x = Mathf.Cos((rotatation+correction) * (Mathf.PI / 180));
		var y = Mathf.Sin((rotatation+correction) * (Mathf.PI / 180)) ;
		var finalPosition = new Vector3(x,y,position.z);

		this.transform.position = finalPosition;
	}
	
}
