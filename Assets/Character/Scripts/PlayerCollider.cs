using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpriteRenderer = null;

    [SerializeField] PlayerDig PlayerDig = null;
    [SerializeField] PlayerMovement PlayerMovement = null;

    public Vector3 SavePoint;
    public float ReviveDelay = 1f;

    bool Dead = false;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (!Dead && collision.collider.CompareTag("Deadly")) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("DEAD");

        Dead = true;
        DeathCounter.Instance.Increment();

        ChangeSpriteRendererColorAlpha(0);

        PlayerDig.enabled = false;
        PlayerMovement.enabled = false;

        Invoke(nameof(Revive), 1f);
    }

    public void Revive() {
        Dead = false;

        ChangeSpriteRendererColorAlpha(1);

        transform.position = SavePoint;

        PlayerDig.enabled = true;
        PlayerMovement.enabled = true;
    }

    void ChangeSpriteRendererColorAlpha(float alpha) {
        Color color = SpriteRenderer.color;
        color.a = alpha;
        SpriteRenderer.color = color;
    }
}
