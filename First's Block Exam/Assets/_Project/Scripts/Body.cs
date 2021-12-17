using UnityEngine;

public class Body : MonoBehaviour
{

    //Возможно скрипт полностью устарел.
    public Transform PreviousSegment;
    public float _minDistance;
    public float Speed;

    private void Awake()
    {
    }
    public void Movement(Transform headposition)
        {
        float distance = (PreviousSegment.position - transform.position).magnitude;
        Vector3 newPosition = PreviousSegment.position;
        newPosition.y = headposition.position.y;
        float lerpT = Time.deltaTime * distance / _minDistance * Speed;
        Debug.Log(lerpT);
        if (lerpT > 0.5f) lerpT = 0.5f;
        transform.position = Vector3.Slerp(transform.position, newPosition, lerpT);
        }
}
