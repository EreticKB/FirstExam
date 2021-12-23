using UnityEngine;
using UnityEngine.UI;

public class SnakeSpeed : MonoBehaviour
{
    public MainMenu Menu;
    public Toggle[] SpeedCheks = new Toggle[3];

    private void Awake()
    {
        SetCheckBox(Menu.Game.SnakeSpeed);
    }

    private void SetCheckBox(int snakeSpeed)
    {
        foreach (Toggle check in SpeedCheks)
        {
            check.isOn = false;
        }
        if (snakeSpeed == 5) SpeedCheks[2].isOn = true;
        if (snakeSpeed == 10) SpeedCheks[1].isOn = true;
        if (snakeSpeed == 15) SpeedCheks[0].isOn = true;
    }
    public void SetSpeed(string name)
    {
        if (name.Equals("Slow")) Menu.Game.SnakeSpeed = 5;
        if (name.Equals("Normal")) Menu.Game.SnakeSpeed = 10;
        if (name.Equals("Fast")) Menu.Game.SnakeSpeed = 15;
        SetCheckBox(Menu.Game.SnakeSpeed);
    }
}
