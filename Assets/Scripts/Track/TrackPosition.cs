using UnityEngine;
using System.Collections;

public class TrackPosition
{
    public int Round { get; private set; }
    public float RoundProgress { get; private set; }
    public Quaternion Rotation { get; private set; }
    //public Vector3 Up { get; private set; }
    public Vector3 Position { get; private set; }

    private Vector3[] points;
    private int index;
    private float d; // the distance traveled on the current line segment

    public TrackPosition(Vector3[] points)
    {
        this.points = points;
        Rotation = Quaternion.LookRotation(points[0].normalized, Vector3.up);
    }

    /// <summary>
    /// Moves the player along the track
    /// </summary>
    /// <param name="distance">Distance moved along the track</param>
    /// <param name="rotateDegrees">Degrees rotated around the track</param>
    /// <returns>The new player position in world space</returns>
    public Vector3 Move(float distance, float rotateDegrees)
    {
        // make up for the distance along the line
        distance += d;
        Vector3 lineSegment = GetLineSegment();

        // find the correct line segment 
        while (distance - lineSegment.magnitude >= 0)
        {
            distance -= lineSegment.magnitude;
            NextPoint();
            lineSegment = GetLineSegment();
        }
        d = distance;
        Vector3 forward = lineSegment.normalized;
        Rotation = Quaternion.LookRotation(forward, Vector3.up);
        Position = points[index] + forward * d;

        return Position;
    }

    // returns a vector from point i and to the next point
    private Vector3 GetLineSegment()
    {
        if (index == points.Length - 1)
            return points[0] - points[index];
        else
            return points[index + 1] - points[index];
    }

    private void NextPoint()
    {
        index++;
        if (index == points.Length)
        {
            // we completet a round on the track
            index = 0;
            Round++;
            RoundProgress = 0f;
        }
    }
}
