using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public ToggleGroup tg;
    public Text label;
    private string difficulty;

    void Start()
    {
        SetBestTimeLabel("BestNormal");
    }

    public void OnToggleClicked()
    {
        difficulty = tg.ActiveToggles().FirstOrDefault().gameObject.name;
        SetBestTimeLabel("Best" + difficulty);
    }

    private void SetBestTimeLabel(string key)
    {
        if (PlayerPrefs.HasKey(key))
            label.text = "Best time:  " + PlayerPrefs.GetString(key);
        else
            label.text = "Best time:  ---";
    }

    public void OnStartBtnClick()
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene("Level1");
    }

    public void OnExitBtnClick()
    {
        print("Quit");
        Application.Quit();
    }
}
