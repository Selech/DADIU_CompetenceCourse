using UnityEngine;
using System.Collections;
using System;

public class TimeTracker : MonoBehaviour {

    private UIManager ui;
    private DateTime startTime;
    private DateTime? endTime;

    void Start()
    {
        ui = GetComponent<UIManager>();
    }

    public void StartTracking()
    {
        startTime = DateTime.Now;
        endTime = null;
        InvokeRepeating("UpdateTimeText", 0, 1);
    }

    public void StopTracking()
    {
        endTime = DateTime.Now;
        CancelInvoke();
    }

    void UpdateTimeText()
    {
        ui.UpdateHUDTime((int)(DateTime.Now - startTime).TotalSeconds);
    }
}
