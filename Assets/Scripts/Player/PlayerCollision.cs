using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    Player player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
	}
	
	void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "PowerupSpeed")
        {
            coll.gameObject.SetActive(false);
            player.ActivateSpeedPowerup();
        }
        else if (coll.transform.tag == "PowerupRotation")
        {
            coll.gameObject.SetActive(false);
            player.ActivateRotationPowerup();
        }
        else
        {

            Camera.main.GetComponent<HitCameraEffect>().TriggerGrayScale();
            player.CollisionEnter();
        }
    }
    
    void OnCollisionExit(Collision coll)
    {
        player.CollisionExit();
    }
}
