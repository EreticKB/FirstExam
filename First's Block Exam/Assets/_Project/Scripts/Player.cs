using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody SnakeHead;
    public float SnakeSensitivity;
    public float SnakeSideForceMax;
    public float ForwardVelocity;

    private Body _body;

    private void Awake()
    {
        _body = GetComponent<Body>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _body.ExtendSnake();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _body.RetractSnake();
        }



    }
    void FixedUpdate()
    {       
        SnakeHeadMovement();
    }

   

    private void SnakeHeadMovement()
    {
        SnakeHead.velocity = Vector3.forward * ForwardVelocity * Time.deltaTime;
        float mousePosition = GetOnPlatformPosition(Input.mousePosition).x;
        if (mousePosition < 13.5f) mousePosition = 13.5f;
        if (mousePosition > 16.9f) mousePosition = 16.9f;
        if (Input.GetMouseButton(0))
        {
            float deltaX = Mathf.InverseLerp(13.6f, 16.9f, mousePosition);
            float targetX = Mathf.Lerp(0.5f, 29.5f, deltaX);
            Vector3 currentSideForce = (new Vector3(targetX, 0, 0) - new Vector3(SnakeHead.position.x, 0, 0)) * SnakeSensitivity;
            if (Mathf.Abs(currentSideForce.x) > SnakeSideForceMax) currentSideForce.x = Mathf.Sign(currentSideForce.x) * SnakeSideForceMax;
            SnakeHead.AddForce(currentSideForce);
        }
    }

    private Vector3 GetOnPlatformPosition(Vector3 rawPosition)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(rawPosition.x, rawPosition.y, 2f));
    }


    
}
