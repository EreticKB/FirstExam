using UnityEngine;

public class Barrier : MonoBehaviour
{
    private float _bumpForce = 1000;
    private void OnCollisionEnter(Collision collision)
    {
        float collisionDeviateX = collision.transform.position.x - transform.position.x;
        if (collisionDeviateX != 0) collision.rigidbody.AddForce(Vector3.right * Mathf.Sign(collisionDeviateX) * _bumpForce);
        else collision.rigidbody.AddForce(Vector3.right * _bumpForce);
    }
}
