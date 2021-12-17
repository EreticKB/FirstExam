using UnityEngine;

public class Body : MonoBehaviour
{

    //Возможно скрипт полностью устарел.
    public Transform PreviousSegment;
    private Vector3 _transformBuffer;
    private float _timeDelay;
    private Rigidbody _thisSegment;
    public float Speed;
    public int TargetDistance;

    private void Awake()
    {
        _transformBuffer = PreviousSegment.position;
        _thisSegment = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        transform.LookAt(_transformBuffer);
        _thisSegment.velocity = Vector3.forward * Speed * Time.deltaTime;
        if ((transform.position - _transformBuffer).magnitude < TargetDistance) _transformBuffer = PreviousSegment.position;
    }
}
