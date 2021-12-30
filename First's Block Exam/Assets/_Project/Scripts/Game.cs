using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Body Body; //нужна для извелечения длины змеи.
    public MainMenu Menu;
    public Player Player;
    public List<int> PlatformAvaibility { get; private set; } = new List<int>();
    public Transform Level;
    public float MoveDelay = .5f;
    public Text ScoreScreen;
    public Text ScoreMenu;
    public GameObject[] Platforms = new GameObject[6];
    private GameObject[] _platformPool = new GameObject[6];
    private int _previousPlatformPoolIndex;
    private int _obsoletePlatformPoolIndex;
    private int _score;

    public enum State
    {
        Playing,
        Menu,
        DefeatDelay
    }
    public State CurrentState { get; private set; }
    
    private readonly string _bestIndex = "Best";
    private readonly string _snakeSpeedIndex = "SnakeSpeed";
    public int Best
    {
        get => PlayerPrefs.GetInt(_bestIndex, 0);
        set
        {
            PlayerPrefs.SetInt(_bestIndex, value);
            PlayerPrefs.Save();
        }
    }
    public int SnakeSpeed
    {
        get => PlayerPrefs.GetInt(_snakeSpeedIndex, 15);
        set
        {
            PlayerPrefs.SetInt(_snakeSpeedIndex, value);
            PlayerPrefs.Save();
        }
    }
    public void GameStart()
    {
        CurrentState = State.Playing;
    }

    public void ShowMenu()
    {
        bool isLost = false;
        if (CurrentState == State.DefeatDelay) isLost = true;
        if (CurrentState == State.Menu) return;
        CurrentState = State.Menu;
        Menu.ShowMenu(isLost);
    }

    private void Awake()
    {
        CurrentState = State.Playing;
        ShowMenu();
        _previousPlatformPoolIndex = -1;
        for (int i = 0; i < Platforms.Length; i++)
        {
            _platformPool[i] = Instantiate(Platforms[i], new Vector3(0, 0, 0), Quaternion.identity, Level);
            PlaneControl platform = _platformPool[i].GetComponent<PlaneControl>();
            platform.PlatformPoolIndex = i;
            platform.Game = this;
            PushPlatform(i);
        }
        PullPlatform(0, -1, 0, 4);
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        ScoreScreen.text = _score.ToString();
        ScoreMenu.text = $"Score: {_score}";
    }

    public void PullPlatform(int poolIndex, int previousPlatformPoolIndex, int platformPositionIndex, int size)
    {
        if (!PlatformAvaibility.Contains(poolIndex))
        {
            Debug.Log("Платформа занята.");
            return;
        }
        _obsoletePlatformPoolIndex = _previousPlatformPoolIndex;
        _previousPlatformPoolIndex = previousPlatformPoolIndex;
        _platformPool[poolIndex].SetActive(true);
        _platformPool[poolIndex].GetComponent<PlaneControl>().ActivatePlatform(platformPositionIndex, size);
        PlatformAvaibility.Remove(poolIndex);
    }

    public void PullPlatform(int poolIndex,int previousPlatformPoolIndex, int platformPositionIndex)
    {
        PullPlatform(poolIndex, previousPlatformPoolIndex, platformPositionIndex, Body.Size);
    }

    public void AddScore()
    {
        _score++;
        UpdateMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) AmDead();
    }

    public void PushPlatform(int poolIndex)
    {
        if (poolIndex == -1) return;
        if (PlatformAvaibility.Contains(poolIndex)) return;
        _platformPool[poolIndex].GetComponent<PlaneControl>().DeActivatePlatform();
        _platformPool[poolIndex].SetActive(false);
        PlatformAvaibility.Add(poolIndex);
    }

    public void PushPlatform()
    {
        PushPlatform(_obsoletePlatformPoolIndex);
    }

    public void WaitDeath()
    {
        CurrentState = State.DefeatDelay;
    }

    public void AmDead()
    {
        if (_score > Best) Best = _score;
        ShowMenu();
    }
}
