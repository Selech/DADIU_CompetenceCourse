using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public GameManager manager;
    public PlayerMovement player;
    public PlayerMovement ai;
    private int position;
    private int round;
    private int labs;

    void Start()
    {
        manager = GameManager.Instance;
        labs = manager.settings.labs;
    }
	
	// Update is called once per frame
	void Update () {
        round = player.Round;

        if (round > ai.Round)
        {
            position = 1;
        }
        else if (round < ai.Round)
        {
            position = 2;
        }
        else
        {
            position = player.Progress > ai.Progress ? 1 : 2;
        }

        if (round > labs)
        {
            manager.PlayerFinished();
        }
        if (ai.Round > labs)
        {
            manager.AIFinished();
        }
	}
}
