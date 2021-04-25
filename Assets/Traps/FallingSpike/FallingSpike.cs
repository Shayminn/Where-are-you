using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2 = null;
    [SerializeField] SpriteRenderer SpriteRenderer = null;
    [SerializeField] Crackable Crackable = null;

    public LayerMask LayerMask;

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
                rb2.gravityScale = 1;
                Falling = true;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (!Respawning && Falling) {

            Crackable.Crack();
            StartCoroutine(Respawn());
        }
    }

    public IEnumerator Respawn() {
        Respawning = true;

        Color color = SpriteRenderer.color;
        color.a = 0;

        SpriteRenderer.color = color;

        rb2.gravityScale = 0;
        transform.position = originalPos;

        yield return new WaitForSeconds(1f);

        while (color.a < 1) {
            color.a += Time.deltaTime;

            SpriteRenderer.color = color;

            yield return null;
        }

        Falling = false;
        Respawning = false;
    }
}
