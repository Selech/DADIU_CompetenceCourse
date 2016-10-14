using UnityEngine;
using System.Collections;

public class TrackPosition
{
    public int Round { get; private set; }
    public float Progress { get; private set; }
    public float Forward { get; private set; }
    public Vector3 Position { get; private set; }

    private Vector3[] points;
    private int index;

    public TrackPosition(Vector3[] points)
    {
        this.points = points;
    }

    /// <summary>
    /// Moves the player along the track
    /// </summary>
    /// <param name="distance">Distance moved along the track</param>
    /// <param name="rotateDegrees">Degrees rotated around the track</param>
    /// <returns>The new player position in world space</returns>
    public Vector3 Move(float distance, float rotateDegrees)
    {

        return Vector3.zero;
    }
}
