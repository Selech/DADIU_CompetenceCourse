using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class GameSettings
{
    public float speedFactor = 1f;
    public int labs = 3;
}

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public GameSettings settings = new GameSettings();
    public Track track;
    public Player player;
    public List<AgentBased_AI> agents;

    void Awake () {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;

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
                    speedFactor = 1.4f,
                    labs = 3
                };
                break;
            case "Korean":
                settings = new GameSettings()
                {
                    speedFactor = 2f,
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
            //player = GameObject.Find("Player").GetComponent<Player>();
            //player.SetTrackPosition(track.CreateTrackPosition());
            agents = GameObject.FindGameObjectsWithTag("AI").Select(x => x.GetComponent<AgentBased_AI>()).ToList();
            agents.ForEach(x => x.SetTrackPosition(track.CreateTrackPosition()));

            UIManager ui = GameObject.Find("Canvas").GetComponent<UIManager>();
            ui.StartCountdown(3);
            Invoke("StartGame", 3);
        }
    }

    private void StartGame()
    {

    }
}
