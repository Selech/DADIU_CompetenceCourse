using UnityEngine;
using System.Collections;

public class TrackPosition
{
    public int Round { get; private set; }
    public float RoundProgress { get; private set; }
    public Vector3 Forward
    {
        get {
            var d0 = GetLineSegment(index).normalized;
            var d1 = GetLineSegment(index + 1).normalized;
            return Vector3.Lerp(d0, d1, 1-d);
        }
    }
    public Vector3 Up {
        get
        {
            var right = Vector3.Cross(Vector3.up, Forward);
            var up = Vector3.Cross(Forward, right);
            return Quaternion.AngleAxis(angle, Forward) * up.normalized;
        }
    }
    public Vector3 Position {
        get
        {
            return points[index] + Forward * d + Up * Track.radius;
        }
    }

    private Vector3[] points;
    private float angle;
    private int index; // index of the current line segment
    private float d; // the distance traveled on the current line segment

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
    public void Move(float distance)
    {
        if (distance <= 0)
            return;

        // make up for the distance along the line
        distance += d;
        Vector3 lineSegment = GetLineSegment(index);

        // find the correct line segment 
        while (distance - lineSegment.magnitude >= 0)
        {
            distance -= lineSegment.magnitude;
            NextPoint();
            lineSegment = GetLineSegment(index);
        }
        d = distance;
    }

    public void Rotate(float degrees)
    {
        angle += degrees;
    }

    // returns a vector from point i and to the next point
    private Vector3 GetLineSegment(int i)
    {
        return points[(i + 1) % points.Length] - points[i % points.Length];
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
