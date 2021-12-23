using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenu : MonoBehaviour
{
    public MainMenu Menu;
    public void GameRestartToMenu()
    {
        Menu.GameRestart(0);
    }
}
