using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Track track;
    public Player[] players;
    public AgentBased_AI[] agents;


    // Use this for initialization
    void Start () {
        foreach (Player player in players)
        {
            player.SetTrackPosition(track.CreateTrackPosition());
        }

        foreach (AgentBased_AI agent in agents)
        {
            agent.SetTrackPosition(track.CreateTrackPosition());
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
