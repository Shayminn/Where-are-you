using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] PlayerDig PlayerDig = null;
    [SerializeField] PlayerMovement PlayerMovement = null;

    public Vector3 SavePoint;
    public float ReviveDelay = 1f;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Deadly")) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("DEAD");

        ChangeSpriteRendererColorAlpha(0);

        PlayerDig.enabled = false;
        PlayerMovement.enabled = false;

        Invoke(nameof(Revive), 1f);
    }

    public void Revive() {
        ChangeSpriteRendererColorAlpha(1);

        PlayerDig.enabled = true;
        PlayerMovement.enabled = true;
    }

    void ChangeSpriteRendererColorAlpha(float alpha) {
        Color color = SpriteRenderer.color;
        color.a = alpha;
        SpriteRenderer.color = color;
    }
}
