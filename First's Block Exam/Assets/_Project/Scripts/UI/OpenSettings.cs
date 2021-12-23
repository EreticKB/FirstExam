using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    public MainMenu Menu;
    
    public void ChangeScreen()
    {
        Menu.MainMenuPanel.SetActive(false);
        Menu.SettingsPanel.SetActive(true);
    }
}
