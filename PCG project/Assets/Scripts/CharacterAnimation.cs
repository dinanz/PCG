using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // actual values -> inspector
    public float moveSpeed = 1f;

    Rigidbody2D _rigidbody;
    Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
//        _animator.Play("_playerDie");
    }

    void FixedUpdate()
    {
        float speed = Input.GetAxis("Horizontal") * moveSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(speed));

        float direction = transform.localScale.x;
        if((speed < 0 && direction > 0) || (speed > 0 && direction < 1))
        {
            transform.localScale *= new Vector2(-1, 1);

        }
    }
}
