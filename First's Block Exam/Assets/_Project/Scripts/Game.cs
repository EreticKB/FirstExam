using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] Platforms = new GameObject[6];
    private GameObject[] _platformPool = new GameObject[6];
    public Transform Level;
    public Material GroundColor;
    private Vector3 _platformOffSet = new Vector3(0,0,70);

    private void Awake()
    {
        
        for (int i = 0; i < Platforms.Length; i++)
        {
            _platformPool[i] = Instantiate(Platforms[i], new Vector3(0, 0, 0), Quaternion.identity, Level);
            _platformPool[i].GetComponent<PlaneControl>().SpawnPlatform.Game = this;
            if (i > 0) _platformPool[i].SetActive(false);
        }
    }

    public void PullPlatform(int index)
    {
        _platformPool[index].transform.position = _platformOffSet * index;
        _platformPool[index].SetActive(true);
    }

    public void PushPlatform(int index)
    {
        if (index < 0) return;
        _platformPool[index].SetActive(false);
    }
}
