using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

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
        countdown.text = "Go!";
        yield return new WaitForSecondsRealtime(1);

        countdown.gameObject.SetActive(false);
        yield break;
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
