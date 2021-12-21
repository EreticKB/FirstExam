using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    public Player SnakeHeadLink;
    private Rigidbody _snakeHead;
    private readonly Vector3 _position = new Vector3(64f, 0.6f, 22f);
    public Body Body;
    [HideInInspector]public float Distance;

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
        Distance = (_snakeHead.position - _position).magnitude;
        if (Distance > Body.HeadDiameter)
        {
            Vector3 direction = (_snakeHead.position - _position).normalized;
            Distance -= Body.HeadDiameter;
            _snakeHead.position = _position + direction * Distance;
        }
    }


    private void GhostHeadMovement()
    {
        _snakeHead.velocity = Vector3.forward * SnakeHeadLink.ForwardVelocity;
        _snakeHead.AddForce(SnakeHeadLink.ThrowForce);
    }



}
