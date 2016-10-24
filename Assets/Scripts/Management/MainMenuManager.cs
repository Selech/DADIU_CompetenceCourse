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
            label.text = "Best tim e:  " + PlayerPrefs.GetString(key);
        else
            label.text = "Best tim e:  ---";
    }

    public void OnStartBtnClick()
    {
        GameManager.singleton.SetDifficulty(difficulty);
        SceneManager.LoadScene("Level1");
    }

    public void OnExitBtnClick()
    {
        Application.Quit();
    }
}
