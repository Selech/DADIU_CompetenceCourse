using UnityEngine;
using System.Collections;

public class AgentCollision : MonoBehaviour {

    AgentBased_AI agent;

	// Use this for initialization
	void Start () {
        agent = GetComponentInParent<AgentBased_AI>();
	}
	
	void OnCollisionEnter(Collision coll)
    {
        var x = transform.InverseTransformPoint(coll.contacts[0].point);
        //print(x);
        if (coll.transform.tag == "Powerup")
        {
            // nothing happens when agent hits!
        }
        else
        {
            //Camera.main.GetComponent<HitCameraEffect>().TriggerGrayScale();
            agent.OnCollisionEnter();
        }

    }
    
    void OnCollisionExit(Collision coll)
    {
        agent.OnCollisionExit();
    }
}
