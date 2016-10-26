using UnityEngine;
using System.Collections;
using System;

public class TimeTracker : MonoBehaviour {

    private UIManager ui;
    private int secondsPassed;

    void Start()
    {
        ui = GetComponent<UIManager>();
    }

    public void StartTracking()
    {
        InvokeRepeating("UpdateTime", 0, 1f);
    }

    public void StopTracking()
    {
        CancelInvoke();

    }

    void UpdateTime()
    {
        ui.UpdateHUDTime(secondsPassed++);
    }

    public string GetTime()
    {
        return string.Format("{0:0}:{1:00}", secondsPassed / 60, secondsPassed % 60);
    }
}
