using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    
    public Text timeLabel;
    public Text countdown;

    public void StartCountdown(int seconds)
    {
        StartCoroutine(Countdown(seconds));
    }

    IEnumerator Countdown(int seconds)
    {
        countdown.gameObject.SetActive(true);

        for (int i = seconds; i > 0; i--)
        {
            countdown.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        GameManager.Instance.IsPaused = false;
        countdown.text = "Go!";
        yield return new WaitForSecondsRealtime(1);

        countdown.gameObject.SetActive(false);
        yield break;
    }
    
    public void UpdateHUDTime(int seconds)
    {
        timeLabel.text = string.Format("Time: {0:00}:{1:00}", seconds / 60, seconds % 60);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	    
	}
}
