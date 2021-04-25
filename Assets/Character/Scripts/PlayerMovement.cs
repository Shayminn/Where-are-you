using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float Speed = 1;
    [SerializeField] PlayerInAir PlayerInAir = null;
    [SerializeField] Rigidbody2D Rb2 = null;
    [SerializeField] Animator Animator = null;

    public LayerMask LayerMask;

    readonly KeyCode MoveLeft = KeyCode.A;
    readonly KeyCode MoveRight = KeyCode.D;
    readonly KeyCode Jump = KeyCode.Space;

    float JumpPower = 3.75f;

    enum Direction {
        Left,
        Right
    }

    // Update is called once per frame
    void Update() {
        if (!Input.GetKey(MoveLeft) && !Input.GetKey(MoveRight) ||
            (Input.GetKey(MoveLeft) && Input.GetKey(MoveRight))) {
            SetAnimatorBools(false, false);
        }
        else {
            if (Input.GetKey(MoveLeft)) {
                Vector3 move = -Vector3.right * Speed * Time.deltaTime;
                if (!RaycastHorizontal(move)) {
                    transform.Translate(-Vector3.right * Speed * Time.deltaTime);
                }

                SetAnimatorBools(true, false);
            }

            if (Input.GetKey(MoveRight)) {
                Vector3 move = Vector3.right * Speed * Time.deltaTime;
                if (!RaycastHorizontal(move)) {
                    transform.Translate(move);
                }

                SetAnimatorBools(false, true);
            }
        }

        if (!PlayerInAir.InAir) {
            if (Input.GetKeyDown(Jump)) {
                Debug.Log("JUMP");
                Rb2.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);   
            }
        }  
    }

    void SetAnimatorBools(bool left, bool right) {
        Animator.SetBool(Direction.Left.ToString(), left);
        Animator.SetBool(Direction.Right.ToString(), right);
    }
    
    bool RaycastHorizontal(Vector3 dir) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.15f, LayerMask);
        if (hit.collider != null) {
            return true;
        }

        return false;
    }
}
