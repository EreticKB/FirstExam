using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Body[] Segm;
    //public LinkedList<Body> Segments; ����� ������� �� ���� � �������
    public Rigidbody SnakeHead;
    public float SnakeSensitivity;
    public float SnakeSideForceMax;
    public float ForwardVelocity;

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
    private void Update()
    {
        /*foreach (Body segment in Segm)
        {
            segment.Movement();
        }*/
    }
    void FixedUpdate()
    {       
        SnakeHeadMovement();
    }

    private void SnakeHeadMovement()
    {
        SnakeHead.velocity = Vector3.forward * ForwardVelocity * Time.deltaTime;
        if (GetOnPlatformPosition(Input.mousePosition).x < 13.5f || GetOnPlatformPosition(Input.mousePosition).x > 16.9f) return;
        if (Input.GetMouseButton(0))
        {
            float deltaX = Mathf.InverseLerp(13.6f, 16.9f, GetOnPlatformPosition(Input.mousePosition).x);
            float targetX = Mathf.Lerp(0.5f, 29.5f, deltaX);
            Vector3 currentSideForce = (new Vector3(targetX, 0, 0) - new Vector3(SnakeHead.position.x, 0, 0)) * SnakeSensitivity;
            
            if (Mathf.Abs(currentSideForce.x) > SnakeSideForceMax) currentSideForce.x = Mathf.Sign(currentSideForce.x) * SnakeSideForceMax;
            Debug.Log(currentSideForce);
            SnakeHead.AddForce(currentSideForce);
        }
    }

    private Vector3 GetOnPlatformPosition(Vector3 rawPosition)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(rawPosition.x, rawPosition.y, 2f));
    }

    
}
