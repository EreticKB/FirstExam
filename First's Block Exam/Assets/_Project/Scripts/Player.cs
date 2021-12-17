using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Body[] Segm;
    //public LinkedList<Body> Segments; потом перейти на него с массива
    public Rigidbody SnakeHead;
    public float SnakeSpeed;
    public float ForwardVelocity;

    /*private void Update()
    {
        /** Использовать когда буду работать с генерацией.
        foreach (Body segment in Segments.Reverse())
        {
            segment.Movement();
        }
        // Для теста пока что использую массив.
        foreach (Body segment in Segm.Reverse())
        {
            segment.Movement();
        }
    }*/
    private void Update()
    {
        foreach (Body segment in Segm)
        {
            segment.Movement();
        }
    }
    void FixedUpdate()
    {
        
        SnakeHeadMovement();
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

    private Vector3 GetOnPlatformPosition(Vector3 rawPosition)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(rawPosition.x, rawPosition.y, 2f));
    }

    
}
