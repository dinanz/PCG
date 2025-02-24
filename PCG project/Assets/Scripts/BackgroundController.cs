using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float parallaxEffect;
    public GameObject cam;

    float camPosition;
    float bgLength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camPosition = transform.position.x;
        bgLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam. transform.position.x * (1 - parallaxEffect);
        transform.position = Vector3.Lerp(transform.position, new Vector3(camPosition + distance, transform.position.y, transform.position.z), Time.deltaTime * 5f);

        if (movement > camPosition + bgLength) { camPosition += bgLength; }
        else if (movement < camPosition - bgLength) { camPosition -= bgLength; }
    }
}
