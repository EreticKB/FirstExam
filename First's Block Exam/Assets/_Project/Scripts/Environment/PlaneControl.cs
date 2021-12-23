using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public Game Game;
    public bool IsStartPlatform;
    public int PlatformPoolIndex;
    private int _platformPositionIndex;
    //====================================
    public GameObject[] Foods = new GameObject[6];
    int[] _foodQuality = new int[6];
    FoodConsumption[] _foodScript = new FoodConsumption[6];
    //====================================
    public GameObject[] Blocks = new GameObject[13];
    int[] _blockQuality = new int[13];
    Blocks[] _blockScript = new Blocks[13];
    //====================================

    private void Awake()
    {
        for (int i = 0; i < Foods.Length; i++)
        {
            _foodScript[i] = Foods[i].GetComponent<FoodConsumption>();
        }

        for (int i = 0; i < Blocks.Length; i++)
        {
            _blockScript[i] = Blocks[i].GetComponent<Blocks>();
        }
    }

    public void TriggerSprung()
    {
        Game.PushPlatform();
        Game.PullPlatform(GetNextPlatformPoolIndex(Game.PlatformAvaibility), PlatformPoolIndex, _platformPositionIndex+1);
    }

    public void ActivatePlatform(int platformPositionIndex, int size)
    {
        _platformPositionIndex = platformPositionIndex;
        //отключаем 3 сложных блока для стартового типа платформы
        if (IsStartPlatform)
        {
            for (int i = 10; i < 13; i++) Blocks[i].SetActive(false);
        }
        //смещаем платформу на нужную позицию согласно ее вызванному номеру
        transform.position = new Vector3(0, 0, platformPositionIndex * 70);
        //назначаем качество еды и препятствий
        for (int i = 0; i < _blockQuality.Length; i++) _blockQuality[i] = 2;
        for (int i = 10; i < _blockQuality.Length; i++) _blockQuality[i] = 4;
        for (int i = 0; i < _foodQuality.Length; i++) _foodQuality[i] = 2;
        RandomQuality(_blockQuality, 0,4,2,2);
        RandomQuality(_blockQuality, 5, 9, 7, 2);
        RandomQuality(_foodQuality, 0, _foodQuality.Length, 3, 2);
        //обновляем блоки согласно качеству.
        for (int i = 0; i < _blockQuality.Length; i++) _blockScript[i].Refresh(_blockQuality[i], size);
        for (int i = 0; i < _foodQuality.Length; i++) _foodScript[i].Refresh(_blockQuality[i]);
    }

    private void RandomQuality(int[] array, int min, int max, int shiftPoint, int shiftValue)
    {
        array[Random.Range(min, max)] = 3;
        int randomIndex = Random.Range(min, max);
        if (array[randomIndex] == 3)
        {
            if (randomIndex < shiftPoint) randomIndex += shiftValue;
            else randomIndex -= shiftValue;
        }
        array[randomIndex] = 1;
    }

    private int GetNextPlatformPoolIndex(List<int> list)
    {
        int index = Random.Range(0, list.Count - 1);
        return list.ElementAt(index);
    }

    public void DeActivatePlatform()
    {
        for (int i = 0; i < Foods.Length; i++) Foods[i].SetActive(true);
        for (int i = 0; i < Blocks.Length; i++) Blocks[i].SetActive(true);
    }
}
