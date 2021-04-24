using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float Speed = 1;
    [SerializeField] Rigidbody2D rb2 = null;
    [SerializeField] Animator animator = null;

    readonly KeyCode MoveLeft = KeyCode.A;
    readonly KeyCode MoveRight = KeyCode.D;
    readonly KeyCode Jump = KeyCode.W;

    float JumpPower = 4f;
    public bool OnGround = false;

    enum Direction {
        Left,
        Right
    }

    // Update is called once per frame
    void FixedUpdate() {
        Debug.Log(Input.GetKey(MoveRight) + " " + Input.GetKey(MoveLeft));

        if (!Input.GetKey(MoveLeft) && !Input.GetKey(MoveRight) ||
            (Input.GetKey(MoveLeft) && Input.GetKey(MoveRight))) {
            SetAnimatorBools(false, false);
        }
        else {
            if (Input.GetKey(MoveLeft)) {
                transform.Translate(-Vector3.right * Speed * Time.fixedDeltaTime);
                SetAnimatorBools(true, false);
            }

            if (Input.GetKey(MoveRight)) {
                transform.Translate(Vector3.right * Speed * Time.fixedDeltaTime);
                SetAnimatorBools(false, true);
            }
        }

        if (OnGround) {
            if (Input.GetKeyDown(Jump)) {
                Debug.Log("JUMP");
                rb2.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
                OnGround = false;
            }
        }  
    }

    void SetAnimatorBools(bool left, bool right) {
        animator.SetBool(Direction.Left.ToString(), left);
        animator.SetBool(Direction.Right.ToString(), right);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        OnGround = true;
    }

}
