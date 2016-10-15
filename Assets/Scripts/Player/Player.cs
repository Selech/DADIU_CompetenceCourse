using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    private TrackPosition trackPosition;
	
    void Reset()
    {
        speed = 0.2f;
    }

	// Update is called once per frame
	void Update () {
        trackPosition.Move(speed);
        transform.position = trackPosition.Position;
	}

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
    }
}
