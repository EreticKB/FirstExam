using UnityEngine;

public class Body : MonoBehaviour
{

    //Возможно скрипт полностью устарел.
    public Transform PreviousSegment;
    //private Vector3 _offSetVector;
    //private float _offSetValue = 0.5f;
    private Vector3 _transformBuffer;
    private float _timeDelay;

    private void Awake()
    {
        _transformBuffer = PreviousSegment.position;
    }
    public void Movement()
        {
        //_offSetVector = (PreviousSegment.localPosition - transform.localPosition).normalized;
        //_offSetVector.y = 0f;
        //_offSetVector.x = 0f;
        //transform.localPosition = PreviousSegment.localPosition - _offSetVector*_offSetValue;
        if (_timeDelay < 0.001) _timeDelay += Time.deltaTime;
        else
        {
            _timeDelay = 0;
            transform.position = _transformBuffer;
            _transformBuffer = PreviousSegment.position;

        }

        }
}
