using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public Game Game;
    public int Index;
    private void OnTriggerEnter(Collider other)
    {
        Game.PullPlatform(Index + 1);
        Game.PushPlatform(Index - 2);   
    }
}
