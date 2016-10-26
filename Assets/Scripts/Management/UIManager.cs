using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public Text positionLabel;
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

    private void SetPositionLabel(int position)
    {
        switch(position)
        {
            case 1:
                positionLabel.text = "1st";
                break;
            case 2:
                positionLabel.text = "2nd";
                break;
            case 3:
                positionLabel.text = "3rd";
                break;
            default:
                positionLabel.text = position + "th";
                break;
        }
    }
}
