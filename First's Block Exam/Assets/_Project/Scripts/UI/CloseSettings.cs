using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettings : MonoBehaviour
{
    public MainMenu Menu;
    public void ChangeScreen()
    {
        Menu.MainMenuPanel.SetActive(true);
        Menu.SettingsPanel.SetActive(false);
    }
}
