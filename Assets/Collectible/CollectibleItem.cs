using UnityEngine;

public class CollectibleItem : MonoBehaviour {
    bool Collected = false;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (!Collected && collision.CompareTag("Player")) {
            Collected = true;
            CollectibleCounter.Instance.IncreaseCounter();

            AudioManager.Instance.PlaySFX(AudioManager.SFX.Heart);

            Destroy(gameObject);
        }
    }
}
