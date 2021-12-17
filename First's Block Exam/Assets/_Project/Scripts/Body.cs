using UnityEngine;

public class Body : MonoBehaviour
{

    //�������� ������ ��������� �������.
    public Transform PreviousSegment;
    private Vector3 _transformBuffer;
    private float _timeDelay;

    private void Awake()
    {
        _transformBuffer = PreviousSegment.position;
    }
    public void Movement()
        {
        if (_timeDelay < 0.1) _timeDelay += Time.deltaTime;
        else
        {
            _timeDelay = 0;
            transform.position = _transformBuffer;
            _transformBuffer = PreviousSegment.position;

        }

        }
}
