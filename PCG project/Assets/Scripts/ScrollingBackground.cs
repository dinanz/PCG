using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollingEffect;
    public GameObject cam;

    float camPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = cam.transform.position.x * scrollingEffect;
        transform.position = new Vector3(camPosition + movement, transform.position.y, transform.position.z);
    }
}
