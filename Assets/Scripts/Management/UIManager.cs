using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{

    public Text finalTimeLabel;
    public Text timeLabel;
    public Text countdown;

    void Start()
    {
        timeLabel.text = CreateTimeString(0);
    }

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
        timeLabel.text = CreateTimeString(seconds);
    }

    public void SetFinalTime(int seconds)
    {
        finalTimeLabel.text = CreateTimeString(seconds);
    }

    private string CreateTimeString(int seconds)
    {
        return string.Format("Time: {0:0}:{1:00}", seconds / 60, seconds % 60);
    }
}
