using StarterAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour {
    public float movSpeed = 5;
    public float movSpeedMax = 15;
    public float movSpeedDecrease = 5;
    public float jumpSpeed = 5;
    public float jumpSpeedDecrease = -5;

    public float checkGroundDist = 0.5f;
    public bool isGrounded = false;
    public bool isJumping = false;

    public Vector2 currDir;
    public Rigidbody2D rb;
    public LayerMask lm;

    public StarterAssetsInputs inputs;


    void OnEnable() {
        if (inputs == null)
            inputs = GetComponent<StarterAssetsInputs>();
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        inputs.Move += Move;
        inputs.Jump += Jump;
    }
    void OnDisable() {
        inputs.Move -= Move;
        inputs.Jump -= Jump;
    }
    void FixedUpdate() {
        if (currDir == Vector2.zero) {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, new Vector2(0, rb.linearVelocity.y), movSpeedDecrease * Time.deltaTime);
        } else {
            rb.AddForce(currDir * movSpeed * Time.deltaTime, ForceMode2D.Force);
        }

        if (!isJumping && rb.linearVelocity.y > 0)
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, new Vector2(rb.linearVelocity.x, jumpSpeedDecrease), movSpeedDecrease * Time.deltaTime);

        if (rb.linearVelocity.x < -movSpeedMax) {
            rb.linearVelocityX = -movSpeedMax;
        } else if (rb.linearVelocity.x > movSpeedMax) {
            rb.linearVelocityX = movSpeedMax;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, checkGroundDist, lm)) {
            isGrounded = true;
        } else isGrounded = false;
        /*
        if (Physics2D.CircleCast(transform.position, checkGroundRadius, Vector2.down, checkGroundDist, lm)) {
            isGrounded = true;
        } else isGrounded = false;
        */
    }

    public void Move(Vector2 mov) {
        if (currDir == mov) return;
        mov.y = 0;
        currDir = mov;
    }

    public void Jump(bool jump) {
        isJumping = jump;
        if (!isGrounded || !isJumping) return;
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    void OnDrawGizmos() {
        /*Gizmos.DrawWireSphere(
            new Vector2(transform.position.x, transform.position.y) + Vector2.down * checkGroundDist,
            checkGroundRadius);*/
    }

}
