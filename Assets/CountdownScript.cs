using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public Text CountdownText;
    public int Count = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var left = Count - (int) Time.timeSinceLevelLoad;
	    if (left <= 0)
	    {
            CountdownText.text = "GO";

            if (left <= -2) { 
                CountdownText.gameObject.SetActive(false);
                this.enabled = false;
            }
        }
	    else
	    {
	        CountdownText.text = left + "";
        }
	}
}
