using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerHija : MonoBehaviour
{ 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null) // o cualquier script que solo tenga el jugador
        {
            SceneManager.LoadScene("OutroScene");
        }
    }
}