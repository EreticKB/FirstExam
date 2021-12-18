using UnityEngine;

public class Body : MonoBehaviour
{

    
    public Transform PreviousSegment;
    private Vector3 _transformBuffer;
    private Rigidbody _thisSegment;
    public float Speed;
    public float TargetDistance;
    public bool DebugCheck;

    private void Awake()
    {
        _transformBuffer = PreviousSegment.position;
        _thisSegment = GetComponent<Rigidbody>();
        
    }
    private void FixedUpdate()
    {
        if (DebugCheck) Debug.Log("FixedUpdate");
        float distance = Mathf.Abs((transform.position - _transformBuffer).sqrMagnitude);
        if (distance < TargetDistance)
        {
            _transformBuffer = PreviousSegment.position;
            _thisSegment.velocity = Vector3.forward * 0;
            if (DebugCheck) Debug.Log("========="+ distance);
        }
        else
        {
            transform.LookAt(_transformBuffer);
            _transformBuffer = PreviousSegment.position;
            _thisSegment.velocity = transform.forward * Speed * Time.deltaTime;
            if (DebugCheck) Debug.Log(distance);
        }
    }
}
