using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Deadly")) {
            Die();
        }
    }

    public void Die() {

    }
}
