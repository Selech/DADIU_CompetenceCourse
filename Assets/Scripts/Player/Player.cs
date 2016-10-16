using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    private TrackPosition trackPosition;

    void Reset()
    {
        speed = 0.45f;
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
        transform.rotation = Quaternion.LookRotation(trackPosition.Forward, trackPosition.Up);
    }

    public void SetTrackPosition(TrackPosition tp)
    {
        trackPosition = tp;
        transform.position = trackPosition.Position;
        transform.forward = trackPosition.Forward;
    }
}
