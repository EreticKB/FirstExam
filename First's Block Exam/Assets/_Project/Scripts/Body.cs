using UnityEngine;

public class Body : MonoBehaviour
{

    //Возможно скрипт полностью устарел.
    public Transform PreviousSegment;
    private float _timeDelay;
    public float Speed;

    public void Movement()
        {
        if (_timeDelay < 0.1) _timeDelay += Time.deltaTime;
        else
        {
            _timeDelay = 0;
            transform.LookAt(PreviousSegment);
            transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self);
        }

        }
}
