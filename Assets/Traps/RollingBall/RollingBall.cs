using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    [SerializeField] RollingDirection RollDirection = RollingDirection.Right;
    [SerializeField] Rigidbody2D rb2 = null;
    [SerializeField] float Force = 25f;
    [SerializeField] bool MoveOnStart = false;

    Vector3 StartingPosition;
    int sign = 1;

    enum RollingDirection {
        Right,
        Left
    }

    void Start() {
        StartingPosition = transform.position;

        if (RollDirection == RollingDirection.Left) {
            sign = -1;
        }

        if (MoveOnStart) {
            StartBall();
        }
    }

    void AddForce() {
        rb2.AddForce(Vector3.right * sign * Force, ForceMode2D.Force);
    }

    public void StartBall() {
        rb2.gravityScale = 1;

        AddForce();
    }

    public void ResetBall() {
        transform.position = StartingPosition;

        StartBall();
    }
}
