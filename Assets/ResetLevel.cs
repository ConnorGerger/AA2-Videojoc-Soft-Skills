using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetLevel : MonoBehaviour {
    void Start() {
        GetComponent<StarterAssetsInputs>().ResetLvl += ResetLvl;
    }
    void OnDisable() {
        GetComponent<StarterAssetsInputs>().ResetLvl -= ResetLvl;
    }

    public void ResetLvl() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
