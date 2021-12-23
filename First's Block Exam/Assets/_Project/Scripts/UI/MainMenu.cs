using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text SessionCounter;
    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject ResurrectMenuPanel;
    public GameObject Counter;
    public Game Game;
    public Text Best;
    public Text BestRecord;

    private readonly string _isDefeatedIndex = "Defeated";
    public int IsDefeated
    {
        get => PlayerPrefs.GetInt(_isDefeatedIndex, 0);
        set
        {
            PlayerPrefs.SetInt(_isDefeatedIndex, value);
            PlayerPrefs.Save();
        }
    }
   
    public void GameStart()
    {
        Counter.SetActive(true);
        MainMenuPanel.SetActive(false);
        Game.GameStart();
    }

    public void ShowMenu(bool lost)
    {
        if (lost) ResurrectMenuPanel.SetActive(true);
        else if (IsDefeated == 0) MainMenuPanel.SetActive(true);
        else GameStart();
        IsDefeated = 0;
        SetScore();
    }
    private void SetScore()
    {
        Best.text = "Best - "+Game.Best.ToString();
        BestRecord.text = "Best: "+Game.Best.ToString();
    }
    public void GameRestart(int isDefeated)
    {
        IsDefeated = isDefeated;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

