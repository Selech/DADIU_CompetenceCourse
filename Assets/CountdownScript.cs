﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public Text CountdownText;
    public int Count;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var left = Count - (int) Time.timeSinceLevelLoad;
	    CountdownText.text = left + "";

	    if (left == 0)
	        print("Game start");
	}
}