using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public MainMenu Menu;

    public void GameRestart()
    {
        Menu.GameRestart(1);
    }
}
