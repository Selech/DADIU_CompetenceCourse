using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSettings
{
    public float speedFactor;
    public int labs;
}

public class GameManager : MonoBehaviour {
    public static GameManager singleton;
    public GameSettings settings;







    public Track track;
    public Player[] players;
    public AgentBased_AI[] agents;

    void Awake () {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;




        

        
        

        
    }

    public void SetDifficulty(string difficulty)
    {
        switch (PlayerPrefs.GetString("Difficulty"))
        {
            case "Hard":
                settings = new GameSettings()
                {
                    speedFactor = 1.5f,
                    labs = 3
                };
                break;
            case "Korean":
                settings = new GameSettings()
                {
                    speedFactor = 2.5f,
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
            // init players and AI's
            foreach (Player player in players)
            {
                player.SetTrackPosition(track.CreateTrackPosition());
            }

            foreach (AgentBased_AI agent in agents)
            {
                agent.SetTrackPosition(track.CreateTrackPosition());
            }
        }
    }
}
