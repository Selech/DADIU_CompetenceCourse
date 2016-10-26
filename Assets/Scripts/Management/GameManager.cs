using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public Player player;
    public List<AI> agents;
    public bool IsPaused = true;
    private bool aiFinish = false;
    private bool playerFinish = false;

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

        IsPaused = true;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            UIManager ui = GameObject.Find("Canvas").GetComponent<UIManager>();
            ui.StartCountdown(3);
            Invoke("StartGame", 3);
        }
    }

    private void StartGame()
    {
        TimeTracker timeTracker = GameObject.Find("Canvas").GetComponent<TimeTracker>();
        timeTracker.StartTracking();
    }

    public void AIFinished()
    {
        if (!aiFinish)
        {
            TimeTracker timeTracker = GameObject.Find("Canvas").GetComponent<TimeTracker>();
            string time = timeTracker.GetTime();

            if (playerFinish)
            {
                GameObject.Find("Canvas").GetComponent<UIScoreManager>().SetSecond(false, time);
                timeTracker.StopTracking();
                GameObject.Find("WaitingForAI").SetActive(false);
            }
            else
            {
                GameObject.Find("Canvas").GetComponent<UIScoreManager>().SetFirst(false, time);
            }

            aiFinish = true;
        }
    }

    public void PlayerFinished()
    {
        if (!playerFinish) { 
            TimeTracker timeTracker = GameObject.Find("Canvas").GetComponent<TimeTracker>();
            string time = timeTracker.GetTime();

            if (aiFinish) { 
                GameObject.Find("Canvas").GetComponent<UIScoreManager>().SetSecond(true, time);
                timeTracker.StopTracking();
            }
            else { 
                GameObject.Find("Canvas").GetComponent<UIScoreManager>().SetFirst(true, time);
                GameObject.Find("WaitingForAI").GetComponent<Text>().text = "Waiting for AI to finish..";
            }

            playerFinish = true;
            GameObject.Find("HUD").SetActive(false);
        }
    }
}
