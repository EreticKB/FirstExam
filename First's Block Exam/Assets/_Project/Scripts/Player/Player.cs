using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody SnakeHead;
    public float SnakeSensitivity;
    public float SnakeSideForceMax;
    public float ForwardVelocity;
    private Collision _currentBlock;


    [HideInInspector] public Vector3 ThrowForce;
    [HideInInspector] public bool Collide;

    private Body _body;

    private void Awake()
    {
        _body = GetComponent<Body>();
    }

    private void Start()
    {
       for (int i = 0; i < 4; i++) _body.ExtendSnake();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _body.ExtendSnake();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _body.RetractSnake(true);
        }
    }
    void FixedUpdate()
    {
        SnakeHeadMovement();
    }



    private void SnakeHeadMovement()
    {
        SnakeHead.velocity = Vector3.forward * ForwardVelocity;
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
            ThrowForce = currentSideForce;
        }
    }

    private Vector3 GetOnPlatformPosition(Vector3 rawPosition)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(rawPosition.x, rawPosition.y, 2f));
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Collide) return;
        if (_currentBlock==null) _currentBlock = collision;
        if (!_currentBlock.collider.TryGetComponent(out Blocks bloks))
        {
            _currentBlock = null;
            return;
        }
        Vector3 collisionNormal = -_currentBlock.contacts[0].normal.normalized;
        float dot = Vector3.Dot(collisionNormal, Vector3.forward);
        if (dot < 0.9f)
        {
            _currentBlock = null;
            return;
        }
        Collide = bloks.GetDamage();
    }


    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.TryGetComponent(out Blocks bloks)) return;
        _currentBlock = null;
    }

    public void Die()
    {
        Debug.Log("Snake dies");
    }
}
