using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    private void Update() {
    if (Input.GetKeyDown(KeyCode.R)) {
        SceneManager.LoadSceneAsync("X");
    }
}
}
