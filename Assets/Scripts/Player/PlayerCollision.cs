using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    Player player;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Player>();
	}
	
	void OnCollisionEnter(Collision coll)
    {
        var x = transform.InverseTransformPoint(coll.contacts[0].point);
        //print(x);
        if (coll.transform.tag == "Powerup")
        {
            coll.gameObject.SetActive(false);
            player.ActivatePowerup();
        }
        else
        {
            Camera.main.GetComponent<HitCameraEffect>().TriggerGrayScale();
            player.OnCollisionEnter();
        }
    }
    
    void OnCollisionExit(Collision coll)
    {
        player.OnCollisionExit();
    }
}
