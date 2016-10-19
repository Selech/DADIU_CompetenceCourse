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
        player.OnCollisionEnter();
    }
    
    void OnCollisionExit(Collision coll)
    {
        player.OnCollisionExit();
    }
}
