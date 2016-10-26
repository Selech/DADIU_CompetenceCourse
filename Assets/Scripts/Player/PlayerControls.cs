using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerControls : MonoBehaviour {

    private IMovable movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
    
	void Update () {
        if (Input.GetAxisRaw("Vertical") > 0)
            movement.Accelerate();
        if (Input.GetAxisRaw("Vertical") < 0)
            movement.Break();
        if (Input.GetAxisRaw("Horizontal") > 0)
            movement.RotateRight();
        if (Input.GetAxisRaw("Horizontal") < 0)
            movement.RotateLeft();
    }
}
