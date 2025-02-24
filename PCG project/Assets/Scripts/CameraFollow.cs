using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public Transform player;
    //public Vector3 offset;

    //private void LateUpdate(){
    //    transform.position = player.transform.position + offset;
    //}

    GameObject player;
    Vector3 offset;

    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    private void Update(){
        transform.position = player.transform.position + offset;
        // transform.LookAt(player.transform.position);
    }
}
