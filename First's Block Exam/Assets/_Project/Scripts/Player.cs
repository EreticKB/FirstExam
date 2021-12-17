using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] Segm; 
    public LinkedList<Transform> Segments;
    public Rigidbody SnakeHead;
    public float SnakeSpeed;
    public float ForwardVelocity;
    public float _distance;
    private float _sqrtDistance;

    /*private void Update()
    {
        /** ������������ ����� ���� �������� � ����������.
        foreach (Body segment in Segments.Reverse())
        {
            segment.Movement();
        }
        // ��� ����� ���� ��� ��������� ������.
        foreach (Body segment in Segm.Reverse())
        {
            segment.Movement();
        }
    }*/
    private void Awake()
    {
        _sqrtDistance = Mathf.Sqrt(_distance);
    }
    void FixedUpdate()
    {
        SnakeHeadMovement();
        SnakeBodyMovement();
    }

    private void SnakeHeadMovement()
    {
        SnakeHead.velocity = Vector3.forward * ForwardVelocity * Time.deltaTime;
        if (GetOnPlatformPosition(Input.mousePosition).x < 2.4f || GetOnPlatformPosition(Input.mousePosition).x > 3.5f) return;
        if (Input.GetMouseButton(0))
        {
            float deltaX = Mathf.InverseLerp(2.6f, 3.4f, GetOnPlatformPosition(Input.mousePosition).x);
            float targetX = Mathf.Lerp(0.25f, 5.75f, deltaX);
            SnakeHead.AddForce((new Vector3(targetX, 0, 0) - new Vector3(SnakeHead.position.x, 0, 0)) * SnakeSpeed);
        }
    }

    private void SnakeBodyMovement()
    {
        Vector3 previousSegmentPosition = transform.localPosition;
        foreach (Transform segment in Segm)
        {
            if ((segment.localPosition - previousSegmentPosition).sqrMagnitude > _sqrtDistance)
            {
                Vector3 currentSegmentPositon = segment.localPosition;
                segment.localPosition = previousSegmentPosition;
                previousSegmentPosition = currentSegmentPositon;
            }
            else break;
        }
    }

    private Vector3 GetOnPlatformPosition(Vector3 rawPosition)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(rawPosition.x, rawPosition.y, 2f));
    }

    
}
