using UnityEngine;


public class CameraFollowPlayer : MonoBehaviour {
    public Transform player;

    public Vector3 offset = new Vector3(0, 3, -10);

    public float speed = 5;


    void Start() {
        player = GameObject.Find("Player").transform;
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, player.position + offset, speed * Time.deltaTime);
    }

}
