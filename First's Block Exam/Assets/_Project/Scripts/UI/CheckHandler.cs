using UnityEngine;

public class CheckHandler : MonoBehaviour
{
    public SnakeSpeed SnakeSpeed;
    public void PickCheckBox()
    {
        SnakeSpeed.SetSpeed(gameObject.name);
    }
}
