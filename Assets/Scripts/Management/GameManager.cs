using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Track track;
    public Player[] players;


	// Use this for initialization
	void Start () {
        foreach (Player player in players)
        {
            player.SetTrackPosition(track.CreateTrackPosition());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
