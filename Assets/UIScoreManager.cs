using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIScoreManager : MonoBehaviour
{
    public Text first;
    public Text second;
    public GameObject scoreScreen;

    public void SetFirst(bool player, string time)
    {
        first.text = player ? time + " - Player" : time + " - AI";
    }

    public void SetSecond(bool player, string time)
    {
        second.text = player ? time + " - Player" : time + " - AI";
        scoreScreen.SetActive(true);
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
