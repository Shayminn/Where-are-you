using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rb2 = null;
    [SerializeField] SpriteRenderer SpriteRenderer = null;
    [SerializeField] PolygonCollider2D PolygonCollider2D = null;
    [SerializeField] Crackable Crackable = null;

    public LayerMask LayerMask;
    public LayerMask HitLayerMask;

    Vector3 originalPos;
    bool Respawning = false;
    bool Falling = false;

    private void Start() {
        originalPos = transform.position;
    }

    void FixedUpdate() {

        if (!Respawning) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, Mathf.Infinity, LayerMask);

            if (hit.collider != null) {
                if (hit.collider.CompareTag("Player")) {
                    Rb2.gravityScale = 1;
                    Falling = true;
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            Rb2.gravityScale = 1;
            Falling = true;
        }

        if (((1 << collision.collider.gameObject.layer) & HitLayerMask) != 0) {
            if (!Respawning && Falling) {
                Crackable.Crack();
                StartCoroutine(Respawn());
            }
        }
    }

    public IEnumerator Respawn() {
        Respawning = true;

        Color color = SpriteRenderer.color;
        color.a = 0;

        SpriteRenderer.color = color;

        PolygonCollider2D.enabled = false;
        Rb2.gravityScale = 0;
        Rb2.velocity = Vector3.zero;
        transform.position = originalPos;

        yield return new WaitForSeconds(1f);

        while (color.a < 1) {
            color.a += Time.deltaTime;

            SpriteRenderer.color = color;

            yield return null;
        }

        PolygonCollider2D.enabled = true;
        Falling = false;
        Respawning = false;
    }
}
