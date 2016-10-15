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
        transform.position = trackPosition.Move(speed, 0f);

	}

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
    }
}
