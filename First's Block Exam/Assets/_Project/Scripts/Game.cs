using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Body Body; //нужна для извелечения длины змеи.
    private int _previousPlatformPoolIndex;
    private int _obsoletePlatformPoolIndex;
    public GameObject[] Platforms = new GameObject[6];
    private GameObject[] _platformPool = new GameObject[6];
    public List<int> PlatformAvaibility { get; private set; } = new List<int>();
    public Transform Level;
    //Раздел под PlayerPrefs и взаимодействие с меню. И нет, я не собираюсь это удалять, чтобы не путаться в коде в дальнейшем.
















    public void ShowMenu()
    {
        CurrentState = State.Menu;
    }

    //======================================
    public enum State
    {
        Playing,
        Menu,
        DefeatDelay
    }
    public State CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = State.Playing;
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
}
