using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class GameSettings
{
    public float speedFactor;
    public int labs;
}

public class GameManager : MonoBehaviour {
    public static GameManager singleton;
    public GameSettings settings;
    public Track track;
    public Player player;
    public List<AgentBased_AI> agents;

    void Awake () {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Hard":
                settings = new GameSettings()
                {
                    speedFactor = 1.2f,
                    labs = 3
                };
                break;
            case "Korean":
                settings = new GameSettings()
                {
                    speedFactor = 1.7f,
                    labs = 4
                };
                break;
            default: // normal is default
                settings = new GameSettings()
                {
                    speedFactor = 1f,
                    labs = 3
                };
                break;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            track = GameObject.Find("Track").GetComponent<Track>();
            player = GameObject.Find("Player").GetComponent<Player>();
            player.SetTrackPosition(track.CreateTrackPosition());
            agents = GameObject.FindGameObjectsWithTag("AI").Select(x => x.GetComponent<AgentBased_AI>()).ToList();
            agents.ForEach(x => x.SetTrackPosition(track.CreateTrackPosition()));
        }
    }
}
