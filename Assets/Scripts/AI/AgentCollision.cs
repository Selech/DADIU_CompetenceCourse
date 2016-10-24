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
        if (coll.transform.tag == "Powerup")
        {
            agent.ActivateSpeedPowerup();
        }
        else if (coll.transform.tag == "PowerupRotation")
        {
            //coll.gameObject.SetActive(false);
            agent.ActivateRotationPowerup();
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
