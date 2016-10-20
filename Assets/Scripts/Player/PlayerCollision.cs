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
        print(x);
        player.OnCollisionEnter();

    }
    
    void OnCollisionExit(Collision coll)
    {
        player.OnCollisionExit();
    }
}
