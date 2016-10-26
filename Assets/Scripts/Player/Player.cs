using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    
    public Material mat;
    public Light internalLight;
    private AudioManager audio;
    private PlayerMovement movement;
    public GameObject ball;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        audio = AudioManager.Instance;
    }

	// Update is called once per frame
	void Update ()
	{
        float speed = movement.RelativeSpeed;
        // set glow based on speed
        mat.SetFloat("_GlowAmount", speed);
	    internalLight.intensity = speed;

        // rescale model based on speed
        var scale = speed > 1f ? speed - 1f + 1.5f : 1f;
        ball.transform.localScale = new Vector3(1f, 1f, scale);
    }
    
    void OnCollisionEnter(Collision coll)
    {
        switch(coll.collider.tag)
        {
            case "Obstacle":
                audio.PlayCollisionSound();
                break;
            case "PowerupSpeed":
                audio.PlayPowerupSound();
                movement.BoostSpeed();
                break;
            case "PowerupRotation":
                audio.PlayPowerupSound();
                movement.BoostRotation();
                break;
        }
    }
}
