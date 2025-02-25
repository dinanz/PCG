using UnityEngine;

public class Guardian_animator : MonoBehaviour
{
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("Animator is null");
        }
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
            _animator.enabled = true;
            _animator.SetTrigger("Entered");
            

        }
    }
}
