using UnityEngine;

public class CrackedPieces : MonoBehaviour {
    Rigidbody2D Rb2;
    PolygonCollider2D PolygonCollider2D;
    public LayerMask LayerMask;

    void Start() {
        Rb2 = GetComponent<Rigidbody2D>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (((1 << collision.collider.gameObject.layer) & LayerMask) != 0) {
            Rb2.gravityScale = 0;
            Rb2.velocity = Vector3.zero;
            Rb2.angularVelocity = 0;

            PolygonCollider2D.isTrigger = true;
        }
    }
}
