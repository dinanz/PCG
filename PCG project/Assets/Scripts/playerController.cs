using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour
{
    // actual values -> inspector
    public float moveSpeed = 2f;

    Rigidbody2D _rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float speed = Input.GetAxis("Horizontal") * moveSpeed;
        _rigidbody.linearVelocity = new Vector2(speed, _rigidbody.linearVelocity.y);

        float direction = transform.localScale.x;
        if((speed < 0 && direction > 0) || (speed > 0 && direction < 1))
        {
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    public float getSpeed() { return moveSpeed; }
}
