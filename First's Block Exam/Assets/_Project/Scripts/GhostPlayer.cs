using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    public Player SnakeHeadLink;
    private Rigidbody _snakeHead;
    public readonly Vector3 Position = new Vector3(64f, 0.6f, 22f);
    public Body Body;

    private void Awake()
    {
        _snakeHead = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GhostHeadMovement();
    }
    private void Update()
    {
        float distance = (_snakeHead.position - Position).magnitude;
        if (distance > Body.HeadDiameter)
        {
            Vector3 direction = (_snakeHead.position - Position).normalized;
            distance -= Body.HeadDiameter;
            _snakeHead.position = Position + direction * distance;
        }
    }


    private void GhostHeadMovement()
    {
        _snakeHead.velocity = Vector3.forward * SnakeHeadLink.ForwardVelocity;
        _snakeHead.AddForce(SnakeHeadLink.ThrowForce);
    }



}
