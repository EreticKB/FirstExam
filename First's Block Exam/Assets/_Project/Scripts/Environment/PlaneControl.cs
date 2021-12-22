using UnityEngine;

public class PlaneControl : MonoBehaviour
{

    public GameObject[] Foods = new GameObject[6];
    int[] _foodQuality = new int[6];
    FoodConsumption[] _foodScript = new FoodConsumption[6];

    public GameObject[] Blocks = new GameObject[12];
    int[] _blockquality = new int[12];
    Blocks[] _blockScript = new Blocks[12];

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
    public SpawnPlatform SpawnPlatform;
}
