using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float Speed = 1;
    [SerializeField] Rigidbody2D rb2 = null;
    [SerializeField] Animator animator = null;

    readonly KeyCode MoveLeft = KeyCode.A;
    readonly KeyCode MoveRight = KeyCode.D;
    readonly KeyCode Jump = KeyCode.W;

    float JumpPower = 4f;
    bool OnGround = false;

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKey(MoveLeft)) {
            transform.Translate(-Vector3.right * Speed * Time.fixedDeltaTime);
            animator.SetBool("MoveLeft");
        }

        if (Input.GetKey(MoveRight)) {
            transform.Translate(Vector3.right * Speed * Time.fixedDeltaTime);
        }

        if (OnGround) {
            if (Input.GetKeyDown(Jump)) {
                Debug.Log("JUMP");
                rb2.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
                OnGround = false;
            }
        }  
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        OnGround = true;
    }
}
