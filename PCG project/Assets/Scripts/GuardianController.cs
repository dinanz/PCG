using UnityEngine;

public class Guardian_animator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_animator == null || _spriteRenderer == null)
        {
            Debug.Log("Animator is null");
        }
        _spriteRenderer.enabled = false;
        _animator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //start animation
        if (other.gameObject.CompareTag("Player"))
        {
            if (_animator == null)
            {
                Debug.Log("Animator is null");
            }
            _spriteRenderer.enabled = true;
            _animator.enabled = true;
            _animator.SetTrigger("Entered");

            if (other.gameObject.transform.position.x <= transform.position.x)
            {
                Debug.Log("Guardian faces left");
                _spriteRenderer.flipX = true; // face left
            }
            else
            {
                Debug.Log("Guardian faces right");
                _spriteRenderer.flipX = false; // face right
            }

        }
    }
}
