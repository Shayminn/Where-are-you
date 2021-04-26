using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    public bool DestroyOnArrival = false;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerCollider>().AssignSavePoint(transform.position);

            if (DestroyOnArrival) {
                Destroy(gameObject);
            }
        }
    }
}
