using UnityEngine;

public class Savepoint : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerCollider>().AssignSavePoint(transform.position);

            Destroy(gameObject);
        }
    }
}
