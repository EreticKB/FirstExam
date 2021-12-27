using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Game Game;
    public GhostPlayer Ghost;
    public Rigidbody SnakeHead;
    public ParticleSystem RamParticles;
    public ParticleSystem DeathParticles;
    public AudioSource Hit;
    public AudioSource Death;
    private float _moveDelay;
    private readonly float SnakeSensitivity = 400f;
    private readonly float SnakeSideForceMax = 2000f;
    private Collision _currentBlock;
    private Body _body;



    private void Awake()
    {
        _body = GetComponent<Body>();
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++) _body.ExtendSnake();
        _moveDelay = Game.MoveDelay;
    }

    void FixedUpdate()
    {
        SnakeHeadMovement();
    }



    private void SnakeHeadMovement()
    {

        if (_moveDelay > 0)
        {
            _moveDelay -= Time.deltaTime;
            return;
        }
        if (Game.CurrentState != Game.State.Playing)
        {
            SnakeHead.velocity = Vector3.zero;
            return;
        }
        SnakeHead.velocity = Vector3.forward * Game.SnakeSpeed;
        Ghost.GhostHeadMovement(Game.SnakeSpeed);
        float mousePosition = GetOnPlatformPosition(Input.mousePosition).x;
        if (mousePosition < 13.8f) mousePosition = 13.8f;
        if (mousePosition > 16.6f) mousePosition = 16.6f;
        if (Input.GetMouseButton(0))
        {
            float deltaX = Mathf.InverseLerp(13.8f, 16.6f, mousePosition);
            float targetX = Mathf.Lerp(0.5f, 29.5f, deltaX);
            Vector3 currentSideForce = (new Vector3(targetX, 0, 0) - new Vector3(SnakeHead.position.x, 0, 0)) * SnakeSensitivity;
            if (Mathf.Abs(currentSideForce.x) > SnakeSideForceMax) currentSideForce.x = Mathf.Sign(currentSideForce.x) * SnakeSideForceMax;
            SnakeHead.AddForce(currentSideForce);
            Ghost.GhostHeadSideForce(currentSideForce);
        }
        
    }

    private Vector3 GetOnPlatformPosition(Vector3 rawPosition)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(rawPosition.x, rawPosition.y, 2f));
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Game.CurrentState != Game.State.Playing) return;
        if (_currentBlock == null)
        {
            _currentBlock = collision;
        }
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
        RamParticles.Play();
        if (_body.Collide) return;
        Hit.Play();
        _body.Collide = bloks.GetDamage();
        _body.RetractSnake();
        Game.AddScore();
    }


    private void OnCollisionExit(Collision collision)
    {
        if (!collision.collider.TryGetComponent(out Blocks bloks)) return;
        _currentBlock = null;
    }

    public void Die()
    {
        if (Game.CurrentState != Game.State.Playing) return;
        Game.WaitDeath();
        DeathParticles.Play();
        _body.DisableHead();
        Death.Play();
        StartCoroutine(DeathOfPlayer());
    }

    IEnumerator DeathOfPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        Game.AmDead();
        gameObject.SetActive(false);
    }
}
