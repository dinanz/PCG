using UnityEngine;
using System.Collections;

public class MerchantController : MonoBehaviour
{
    public GameObject chatBox;
    public GameObject mushroom;
    public GameObject instructions;

    bool hasEntered;
    bool isWelcomed;
    bool showMushroom;  // to stop show if press y/n before show
    bool hasMushroom;

    Animator _animator;
    Animator _player;
    Animator _playerChat;
    SpriteRenderer _mushroom;
    GameObject _playerChatBox;
    playerController _playerController;
    PlayerSelector playerSelector;

    int welcomeWeight = 70;
    int refuseWeight = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = chatBox.GetComponent<Animator>();
        _animator.enabled = false;
        _mushroom = mushroom.GetComponent<SpriteRenderer>();
        _mushroom.enabled = false;
        playerSelector = FindObjectOfType<PlayerSelector>();
        _player = playerSelector.getPlayer().GetComponent<Animator>();
        _playerChatBox = playerSelector.getPlayerChat();
        _playerChat = playerSelector.getPlayerChat().GetComponent<Animator>();
        _playerChat.enabled = false;
        _playerController = playerSelector.getPlayerController();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && hasEntered) {
            DetermineMerchantAction();
        }
        if (Input.GetKeyDown(KeyCode.Y) && isWelcomed) {
            showMushroom = false;
            _mushroom.enabled = false;
            _animator.SetTrigger("Smile");
            hasMushroom = true;
            StartCoroutine(CompleteTransaction());
        }
        if (Input.GetKeyDown(KeyCode.N) && isWelcomed) {
            showMushroom = false;
            _mushroom.enabled = false;
            _animator.SetTrigger("NoSmile");
            StartCoroutine(CompleteTransaction());

        }
        if (Input.GetKeyDown(KeyCode.Return) && !hasEntered && hasMushroom) {
            StartCoroutine(UseMushroom());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            _animator.enabled = true;
            ResetTriggers();
            hasEntered = true;
            _animator.SetTrigger("Entered");
            instructions.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            _mushroom.enabled = false;
            hasEntered = false;
            _animator.SetTrigger("Exited");
            instructions.SetActive(false);
        }
    }

    void DetermineMerchantAction()
    {
        int totalWeight = welcomeWeight + refuseWeight;
        int randomValue = Random.Range(0, totalWeight);

        isWelcomed = randomValue < welcomeWeight;

        if (isWelcomed) {
            showMushroom = true;
            StartCoroutine(StartInteraction());
        } else {
            showMushroom = false;
            StartCoroutine(RefuseInteraction());
        }
    }

    IEnumerator StartInteraction()
    {
        _animator.SetTrigger("Welcome");
        yield return new WaitForSeconds(1.5f);
        if (showMushroom && hasEntered) { _mushroom.enabled = true; }
        _animator.SetTrigger("Mushroom");
    }

    IEnumerator RefuseInteraction()
    {
        _animator.SetTrigger("Frown");
        yield return new WaitForSeconds(1.5f);
        _animator.SetTrigger("Done");
    }

    IEnumerator CompleteTransaction()
    {
        yield return new WaitForSeconds(1.5f);
        _animator.SetTrigger("Done");
    }

    IEnumerator UseMushroom()
    {
        yield return new WaitForSeconds(1.5f);
        _player.SetTrigger("Hurt");
        _playerController.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        _playerController.enabled = false;
        _player.SetTrigger("Die");
        yield return new WaitForSeconds(2f);
        _playerChatBox.SetActive(true);
        _playerChat.enabled = true;
        _playerChat.SetTrigger("Speechless");
        yield return new WaitForSeconds(2f);
        _playerChat.SetTrigger("Exit");
        yield return new WaitForSeconds(2f);
        _player.SetTrigger("Spawn");
        yield return new WaitForSeconds(1.5f);
        _playerController.enabled = true;
        _playerChatBox.SetActive(false);
        hasMushroom = false;
    }

    void ResetTriggers(){
         _animator.ResetTrigger("Entered");
         _animator.ResetTrigger("Exited");
         _animator.ResetTrigger("Welcome");
         _animator.ResetTrigger("Mushroom");
         _animator.ResetTrigger("Smile");
         _animator.ResetTrigger("NoSmile");
         _animator.ResetTrigger("Done");
    }
}
