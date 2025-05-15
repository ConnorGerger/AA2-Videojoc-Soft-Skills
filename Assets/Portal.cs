using UnityEngine;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour {
    public int sceneId;


    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "Player") {
            SwapScene();
        }

    }

    public void SwapScene() {
        SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
    }

}
