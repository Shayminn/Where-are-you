using UnityEngine;

public class RollingBall : ResetOnDeath {
    [SerializeField] Rigidbody2D rb2 = null;
    [SerializeField] RollingDirection RollDirection = RollingDirection.Right;
    [SerializeField] float Force = 25f;
    [SerializeField] bool MoveOnStart = false;

    bool Triggered = false;
    int Sign = 1;

    enum RollingDirection {
        Right,
        Left
    }

    new void Start() {
        base.Start();

        if (RollDirection == RollingDirection.Left) {
            Sign = -1;
        }

        if (MoveOnStart) {
            Invoke(nameof(StartTrap), 0.1f);
        }
    }

    void AddForce() {
        rb2.AddForce(Vector3.right * Sign * Force, ForceMode2D.Force);
    }

    public override void StartTrap() {
        Started = true;

        rb2.gravityScale = 1;
        AddForce();
    }

    public override void ResetTrap() {
        transform.position = StartingPosition;
        transform.rotation = Quaternion.identity;

        rb2.velocity = Vector3.zero;
        rb2.angularVelocity = 0;
        rb2.gravityScale = 0;

        if (MoveOnStart) {
            StartTrap();
        }
        else {
            Triggered = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (!MoveOnStart && !Triggered && collision.CompareTag("Player")) {
            Triggered = true;
            StartTrap();
        }
    }
}
