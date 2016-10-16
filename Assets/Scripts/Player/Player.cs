using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
	public float rotateSpeed;
    private TrackPosition trackPosition;

	public GameObject parent;
	
    void Reset()
    {
        speed = 0.3f;
        rotateSpeed = 2.5f;
    }

	// Update is called once per frame
	void Update () {
		trackPosition.Move(0.2f);

        // move player
        float dist = Input.GetAxis("Vertical") * speed;
        trackPosition.Move(dist);
        // rotate player
        var degrees = -Input.GetAxis("Horizontal") * rotateSpeed;
        trackPosition.Rotate(degrees);

		transform.position = trackPosition.Position;
        transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);// Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up), 4);

	}

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
        transform.forward = trackPosition.Forward;
    }

}







////////////////////////////////////////////////
// Warning, old code below!
////////////////////////////////////////////////

public class Player2 : MonoBehaviour
{
    private Vector3 position = Vector3.zero; 
	public float forwardSpeed;
	public float rotatationSpeed;
	public float correction;

	private float rotatation = 0.0f;
    private TrackPosition trackPosition;

    public float speed;
    public float rotateSpeed;

    void Reset()
    {
        speed = 0.3f;
        rotateSpeed = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // move player
        float dist = Input.GetAxis("Vertical") * speed;
        trackPosition.Move(dist);
        // rotate player
        var degrees = -Input.GetAxis("Horizontal") * rotateSpeed;
        trackPosition.Rotate(degrees);

        // update position and rotation
        transform.position = trackPosition.Position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up), 4);
    }

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
        transform.forward = trackPosition.Forward;
    }

	

	private void SetRotation(){
		var tempRotation = rotatation;
		// Get inputs
		if(Input.GetKey(KeyCode.LeftArrow)){
			trackPosition.Rotate(rotatationSpeed);

			 tempRotation += rotatationSpeed;
		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			tempRotation += -rotatationSpeed;
			trackPosition.Rotate(-rotatationSpeed);
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
